using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace LinqToQuerystring.AntlrGenerated
{
    internal interface ILinqToQuerystringVisitor<out TResult> : IParseTreeVisitor<TResult>
    {
        TResult VisitQuery([NotNull] LinqToQuerystringParser.QueryContext context);
        TResult VisitQueryFunction([NotNull] LinqToQuerystringParser.QueryFunctionContext context);
        TResult VisitTop([NotNull] LinqToQuerystringParser.TopContext context);
        TResult VisitSkip([NotNull] LinqToQuerystringParser.SkipContext context);
        TResult VisitOrderBy([NotNull] LinqToQuerystringParser.OrderByContext context);
        TResult VisitOrderByExpression([NotNull] LinqToQuerystringParser.OrderByExpressionContext context);
        TResult VisitOrderByAtom([NotNull] LinqToQuerystringParser.OrderByAtomContext context);
        TResult VisitSelect([NotNull] LinqToQuerystringParser.SelectContext context);
        TResult VisitSelectExpression([NotNull] LinqToQuerystringParser.SelectExpressionContext context);
        TResult VisitWhere([NotNull] LinqToQuerystringParser.WhereContext context);
        TResult VisitWhereExpression([NotNull] LinqToQuerystringParser.WhereExpressionContext context);
        TResult VisitWhereAtom([NotNull] LinqToQuerystringParser.WhereAtomContext context);
        TResult VisitBoolExpression([NotNull] LinqToQuerystringParser.BoolExpressionContext context);
        TResult VisitAtom([NotNull] LinqToQuerystringParser.AtomContext context);
        TResult VisitProperty([NotNull] LinqToQuerystringParser.PropertyContext context);
        TResult VisitConstant([NotNull] LinqToQuerystringParser.ConstantContext context);
        TResult VisitFunction([NotNull] LinqToQuerystringParser.FunctionContext context);
    }
}
