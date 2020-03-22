using System.Linq;
using System.Linq.Expressions;
using LinqToQuerystring.AntlrGenerated;

namespace LinqToQuerystring.Visitors
{
    internal class QueryVisitor : LinqToQuerystringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public QueryVisitor(IQueryable sourceQueryable)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = Expression.Parameter(sourceQueryable.ElementType, "x");
        }

        public override IQueryable VisitQuery(LinqToQuerystringParser.QueryContext context)
        {
            foreach (var function in context.queryFunction())
            {
                _sourceQueryable = function.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitQueryFunction(LinqToQuerystringParser.QueryFunctionContext context)
        {
            foreach (var child in context.children)
            {
                _sourceQueryable = child.Accept(this) ?? _sourceQueryable;
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitTop(LinqToQuerystringParser.TopContext context)
        {
            var visitor = new TopVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSkip(LinqToQuerystringParser.SkipContext context)
        {
            var visitor = new SkipVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitWhere(LinqToQuerystringParser.WhereContext context)
        {
            var visitor = new WhereVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitOrderBy(LinqToQuerystringParser.OrderByContext context)
        {
            var visitor = new OrderByVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }

        public override IQueryable VisitSelect(LinqToQuerystringParser.SelectContext context)
        {
            var visitor = new SelectVisitor(_sourceQueryable, _parameter);
            return context.Accept(visitor);
        }
    }
}
