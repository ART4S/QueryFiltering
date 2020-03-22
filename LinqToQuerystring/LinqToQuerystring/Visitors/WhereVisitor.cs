using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Visitors
{
    internal class WhereVisitor : LinqToQuerystringBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public WhereVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitWhere(LinqToQuerystringParser.WhereContext context)
        {
            var visitor = new WhereExpressionVisitor(_parameter);
            BaseNode whereExpressionNode = context.expression.Accept(visitor);

            MethodInfo lambda = TypeCashe.Expression.LambdaFunc(_parameter.Type, typeof(bool));

            object expression = lambda.Invoke(
                null,
                new object[] { whereExpressionNode.CreateExpression(), new ParameterExpression[] { _parameter } });

            MethodInfo where = TypeCashe.Queryable.Where(_parameter.Type);

            return (IQueryable)where.Invoke(null, new object[] { _sourceQueryable, expression });
        }
    }
}
