using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace QueryFiltering.AntlrGenerated
{
    internal interface IQueryFilteringVisitor<out TResult> : IParseTreeVisitor<TResult>
    {
        TResult VisitQuery([NotNull] QueryFilteringParser.QueryContext context);
        TResult VisitQueryFunction([NotNull] QueryFilteringParser.QueryFunctionContext context);
        TResult VisitTop([NotNull] QueryFilteringParser.TopContext context);
        TResult VisitSkip([NotNull] QueryFilteringParser.SkipContext context);
        TResult VisitOrderBy([NotNull] QueryFilteringParser.OrderByContext context);
        TResult VisitOrderByExpression([NotNull] QueryFilteringParser.OrderByExpressionContext context);
        TResult VisitOrderByAtom([NotNull] QueryFilteringParser.OrderByAtomContext context);
        TResult VisitSelect([NotNull] QueryFilteringParser.SelectContext context);
        TResult VisitSelectExpression([NotNull] QueryFilteringParser.SelectExpressionContext context);
        TResult VisitWhere([NotNull] QueryFilteringParser.WhereContext context);
        TResult VisitWhereExpression([NotNull] QueryFilteringParser.WhereExpressionContext context);
        TResult VisitWhereAtom([NotNull] QueryFilteringParser.WhereAtomContext context);
        TResult VisitBoolExpression([NotNull] QueryFilteringParser.BoolExpressionContext context);
        TResult VisitAtom([NotNull] QueryFilteringParser.AtomContext context);
        TResult VisitProperty([NotNull] QueryFilteringParser.PropertyContext context);
        TResult VisitConstant([NotNull] QueryFilteringParser.ConstantContext context);
        TResult VisitFunction([NotNull] QueryFilteringParser.FunctionContext context);
    }
}
