using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace QueryFiltering.AntlrGenerated
{
    internal class QueryFilteringBaseVisitor<TResult> : AbstractParseTreeVisitor<TResult>, IQueryFilteringVisitor<TResult>
    {
        public virtual TResult VisitQuery([NotNull] QueryFilteringParser.QueryContext context) => VisitChildren(context);
        public virtual TResult VisitQueryFunction([NotNull] QueryFilteringParser.QueryFunctionContext context) => VisitChildren(context);
        public virtual TResult VisitTop([NotNull] QueryFilteringParser.TopContext context) => VisitChildren(context);
        public virtual TResult VisitSkip([NotNull] QueryFilteringParser.SkipContext context) => VisitChildren(context);
        public virtual TResult VisitOrderBy([NotNull] QueryFilteringParser.OrderByContext context) => VisitChildren(context);
        public virtual TResult VisitOrderByExpression([NotNull] QueryFilteringParser.OrderByExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitOrderByAtom([NotNull] QueryFilteringParser.OrderByAtomContext context) => VisitChildren(context);
        public virtual TResult VisitSelect([NotNull] QueryFilteringParser.SelectContext context) => VisitChildren(context);
        public virtual TResult VisitSelectExpression([NotNull] QueryFilteringParser.SelectExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitWhere([NotNull] QueryFilteringParser.WhereContext context) => VisitChildren(context);
        public virtual TResult VisitWhereExpression([NotNull] QueryFilteringParser.WhereExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitWhereAtom([NotNull] QueryFilteringParser.WhereAtomContext context) => VisitChildren(context);
        public virtual TResult VisitBoolExpression([NotNull] QueryFilteringParser.BoolExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitAtom([NotNull] QueryFilteringParser.AtomContext context) => VisitChildren(context);
        public virtual TResult VisitProperty([NotNull] QueryFilteringParser.PropertyContext context) => VisitChildren(context);
        public virtual TResult VisitConstant([NotNull] QueryFilteringParser.ConstantContext context) => VisitChildren(context);
        public virtual TResult VisitFunction([NotNull] QueryFilteringParser.FunctionContext context) => VisitChildren(context);
    }
}
