using Antlr4.Runtime.Tree;
using QueryFiltering.AntlrGenerated;
using QueryFiltering.Exceptions;
using QueryFiltering.Nodes;
using QueryFiltering.Nodes.Aggregates;
using QueryFiltering.Nodes.Base;
using QueryFiltering.Nodes.DataTypes;
using QueryFiltering.Nodes.Functions;
using QueryFiltering.Nodes.Operators;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class WhereExpressionVisitor : QueryFilteringBaseVisitor<BaseNode>
    {
        private readonly ParameterExpression _parameter;

        public WhereExpressionVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        public override BaseNode VisitWhereExpression(QueryFilteringParser.WhereExpressionContext context)
        {
            BaseNode resultNode = context.children[0].Accept(this);

            for (int i = 1; i < context.children.Count; i += 2)
            {
                BaseNode left = resultNode;
                BaseNode right = context.children[i + 1].Accept(this);

                var aggregateNode = (ITerminalNode)context.children[i];

                switch (aggregateNode.Symbol.Type)
                {
                    case QueryFilteringLexer.AND:
                        resultNode = new AndNode(left, right);
                        continue;
                    case QueryFilteringLexer.OR:
                        resultNode = new OrNode(left, right);
                        continue;
                    default:
                        throw new ParseRuleException(
                            nameof(WhereExpressionVisitor),
                            $"Unknown predicate type: '{aggregateNode.Symbol.Type}'");
                }
            }

            return resultNode;
        }

        public override BaseNode VisitWhereAtom(QueryFilteringParser.WhereAtomContext context)
        {
            BaseNode resultNode = context.boolExpr?.Accept(this) ??
                                  context.whereExpr?.Accept(this) ??
                                  throw new ParseRuleException(nameof(WhereExpressionVisitor));

            if (context.not != null)
            {
                resultNode = new NotNode(resultNode);
            }

            return resultNode;
        }

        public override BaseNode VisitAtom(QueryFilteringParser.AtomContext context)
        {
            foreach (IParseTree child in context.children)
            {
                BaseNode node = child.Accept(this);
                if (node != null)
                {
                    return node;
                }
            }

            throw new ParseRuleException(nameof(WhereExpressionVisitor));
        }

        public override BaseNode VisitBoolExpression(QueryFilteringParser.BoolExpressionContext context)
        {
            BaseNode left = context.left.Accept(this);
            BaseNode right = context.right.Accept(this);

            switch (context.operation.Type)
            {
                case QueryFilteringLexer.EQUALS:
                    return new EqualsNode(left, right);
                case QueryFilteringLexer.NOTEQUALS:
                    return new NotEqualsNode(left, right);
                case QueryFilteringLexer.GREATERTHAN:
                    return new GreaterThanNode(left, right);
                case QueryFilteringLexer.GREATERTHANOREQUAL:
                    return new GreaterThanOrEqualNode(left, right);
                case QueryFilteringLexer.LESSTHAN:
                    return new LessThanNode(left, right);
                case QueryFilteringLexer.LESSTHANOREQUAL:
                    return new LessThanOrEqualNode(left, right);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown operation type: '{context.operation.Type}'");
            }
        }

        public override BaseNode VisitProperty(QueryFilteringParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override BaseNode VisitConstant(QueryFilteringParser.ConstantContext context)
        {
            switch (context.value.Type)
            {
                case QueryFilteringLexer.INT:
                    return new IntNode(context.value.Text);
                case QueryFilteringLexer.LONG:
                    return new LongNode(context.value.Text);
                case QueryFilteringLexer.DOUBLE:
                    return new DoubleNode(context.value.Text);
                case QueryFilteringLexer.FLOAT:
                    return new FloatNode(context.value.Text);
                case QueryFilteringLexer.DECIMAL:
                    return new DecimalNode(context.value.Text);
                case QueryFilteringLexer.BOOL:
                    return new BoolNode(context.value.Text);
                case QueryFilteringLexer.GUID:
                    return new GuidNode(context.value.Text);
                case QueryFilteringLexer.NULL:
                    return new NullNode();
                case QueryFilteringLexer.STRING:
                    return new StringNode(context.value.Text);
                case QueryFilteringLexer.DATETIME:
                    return new DateTimeNode(context.value.Text);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown data type: '{context.value.Type}'");
            }
        }

        public override BaseNode VisitFunction(QueryFilteringParser.FunctionContext context)
        {
            BaseNode[] parameters = context.atom().Select(x => x.Accept(this)).ToArray();

            switch (context.value.Type)
            {
                case QueryFilteringParser.TOUPPER:
                    return new ToUpperNode(parameters);
                case QueryFilteringParser.TOLOWER:
                    return new ToLowerNode(parameters);
                case QueryFilteringParser.STARTSWITH:
                    return new StartsWithNode(parameters);
                case QueryFilteringParser.ENDSWITH:
                    return new EndsWithNode(parameters);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown function type: '{context.value.Type}'");
            }
        }
    }
}
