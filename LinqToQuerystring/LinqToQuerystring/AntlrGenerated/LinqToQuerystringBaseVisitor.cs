using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace LinqToQuerystring.AntlrGenerated
{
    internal class LinqToQuerystringBaseVisitor<TResult> : AbstractParseTreeVisitor<TResult>, ILinqToQuerystringVisitor<TResult>
    {
        public virtual TResult VisitQuery([NotNull] LinqToQuerystringParser.QueryContext context) => VisitChildren(context);
        public virtual TResult VisitQueryFunction([NotNull] LinqToQuerystringParser.QueryFunctionContext context) => VisitChildren(context);
        public virtual TResult VisitTop([NotNull] LinqToQuerystringParser.TopContext context) => VisitChildren(context);
        public virtual TResult VisitSkip([NotNull] LinqToQuerystringParser.SkipContext context) => VisitChildren(context);
        public virtual TResult VisitOrderBy([NotNull] LinqToQuerystringParser.OrderByContext context) => VisitChildren(context);
        public virtual TResult VisitOrderByExpression([NotNull] LinqToQuerystringParser.OrderByExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitOrderByAtom([NotNull] LinqToQuerystringParser.OrderByAtomContext context) => VisitChildren(context);
        public virtual TResult VisitSelect([NotNull] LinqToQuerystringParser.SelectContext context) => VisitChildren(context);
        public virtual TResult VisitSelectExpression([NotNull] LinqToQuerystringParser.SelectExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitWhere([NotNull] LinqToQuerystringParser.WhereContext context) => VisitChildren(context);
        public virtual TResult VisitWhereExpression([NotNull] LinqToQuerystringParser.WhereExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitWhereAtom([NotNull] LinqToQuerystringParser.WhereAtomContext context) => VisitChildren(context);
        public virtual TResult VisitBoolExpression([NotNull] LinqToQuerystringParser.BoolExpressionContext context) => VisitChildren(context);
        public virtual TResult VisitAtom([NotNull] LinqToQuerystringParser.AtomContext context) => VisitChildren(context);
        public virtual TResult VisitProperty([NotNull] LinqToQuerystringParser.PropertyContext context) => VisitChildren(context);
        public virtual TResult VisitConstant([NotNull] LinqToQuerystringParser.ConstantContext context) => VisitChildren(context);
        public virtual TResult VisitFunction([NotNull] LinqToQuerystringParser.FunctionContext context) => VisitChildren(context);
    }
}
