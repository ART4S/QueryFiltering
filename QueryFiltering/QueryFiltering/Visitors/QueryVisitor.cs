using QueryFiltering.AntlrGenerated;
using System.Linq;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class QueryVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = Expression.Parameter(sourceQueryable.ElementType, "x");
        }

        public override IQueryable VisitQuery(QueryFilteringParser.QueryContext context)
        {
            foreach (var function in context.queryFunction())
            {
                _sourceQueryable = function.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitQueryFunction(QueryFilteringParser.QueryFunctionContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this) ?? _sourceQueryable;
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitTop(QueryFilteringParser.TopContext context)
        {
            var visitor = new TopVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSkip(QueryFilteringParser.SkipContext context)
        {
            var visitor = new SkipVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitWhere(QueryFilteringParser.WhereContext context)
        {
            var visitor = new WhereVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSelect(QueryFilteringParser.SelectContext context)
        {
            var visitor = new SelectVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }
    }
}
