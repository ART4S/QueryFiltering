using System.Linq;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;
using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Exceptions;
using LinqToQuerystring.Nodes;
using LinqToQuerystring.Nodes.Aggregates;
using LinqToQuerystring.Nodes.Base;
using LinqToQuerystring.Nodes.DataTypes;
using LinqToQuerystring.Nodes.Functions;
using LinqToQuerystring.Nodes.Operators;

namespace LinqToQuerystring.Visitors
{
    internal class WhereExpressionVisitor : LinqToQuerystringBaseVisitor<BaseNode>
    {
        private readonly ParameterExpression _parameter;

        public WhereExpressionVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        public override BaseNode VisitWhereExpression(LinqToQuerystringParser.WhereExpressionContext context)
        {
            BaseNode resultNode = context.children[0].Accept(this);

            for (int i = 1; i < context.children.Count; i += 2)
            {
                BaseNode left = resultNode;
                BaseNode right = context.children[i + 1].Accept(this);

                var aggregateNode = (ITerminalNode)context.children[i];

                switch (aggregateNode.Symbol.Type)
                {
                    case LinqToQuerystringLexer.AND:
                        resultNode = new AndNode(left, right);
                        continue;
                    case LinqToQuerystringLexer.OR:
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

        public override BaseNode VisitWhereAtom(LinqToQuerystringParser.WhereAtomContext context)
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

        public override BaseNode VisitAtom(LinqToQuerystringParser.AtomContext context)
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

        public override BaseNode VisitBoolExpression(LinqToQuerystringParser.BoolExpressionContext context)
        {
            BaseNode left = context.left.Accept(this);
            BaseNode right = context.right.Accept(this);

            switch (context.operation.Type)
            {
                case LinqToQuerystringLexer.EQUALS:
                    return new EqualsNode(left, right);
                case LinqToQuerystringLexer.NOTEQUALS:
                    return new NotEqualsNode(left, right);
                case LinqToQuerystringLexer.GREATERTHAN:
                    return new GreaterThanNode(left, right);
                case LinqToQuerystringLexer.GREATERTHANOREQUAL:
                    return new GreaterThanOrEqualNode(left, right);
                case LinqToQuerystringLexer.LESSTHAN:
                    return new LessThanNode(left, right);
                case LinqToQuerystringLexer.LESSTHANOREQUAL:
                    return new LessThanOrEqualNode(left, right);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown operation type: '{context.operation.Type}'");
            }
        }

        public override BaseNode VisitProperty(LinqToQuerystringParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override BaseNode VisitConstant(LinqToQuerystringParser.ConstantContext context)
        {
            switch (context.value.Type)
            {
                case LinqToQuerystringLexer.INT:
                    return new IntNode(context.value.Text);
                case LinqToQuerystringLexer.LONG:
                    return new LongNode(context.value.Text);
                case LinqToQuerystringLexer.DOUBLE:
                    return new DoubleNode(context.value.Text);
                case LinqToQuerystringLexer.FLOAT:
                    return new FloatNode(context.value.Text);
                case LinqToQuerystringLexer.DECIMAL:
                    return new DecimalNode(context.value.Text);
                case LinqToQuerystringLexer.BOOL:
                    return new BoolNode(context.value.Text);
                case LinqToQuerystringLexer.GUID:
                    return new GuidNode(context.value.Text);
                case LinqToQuerystringLexer.NULL:
                    return new NullNode();
                case LinqToQuerystringLexer.STRING:
                    return new StringNode(context.value.Text);
                case LinqToQuerystringLexer.DATETIME:
                    return new DateTimeNode(context.value.Text);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown data type: '{context.value.Type}'");
            }
        }

        public override BaseNode VisitFunction(LinqToQuerystringParser.FunctionContext context)
        {
            BaseNode[] parameters = context.atom().Select(x => x.Accept(this)).ToArray();

            switch (context.value.Type)
            {
                case LinqToQuerystringParser.TOUPPER:
                    return new ToUpperNode(parameters);
                case LinqToQuerystringParser.TOLOWER:
                    return new ToLowerNode(parameters);
                case LinqToQuerystringParser.STARTSWITH:
                    return new StartsWithNode(parameters);
                case LinqToQuerystringParser.ENDSWITH:
                    return new EndsWithNode(parameters);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown function type: '{context.value.Type}'");
            }
        }
    }
}
