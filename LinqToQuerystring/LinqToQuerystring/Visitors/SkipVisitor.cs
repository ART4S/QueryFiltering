using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Helpers;

namespace LinqToQuerystring.Visitors
{
    internal class SkipVisitor : LinqToQuerystringBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SkipVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitSkip(LinqToQuerystringParser.SkipContext context)
        {
            MethodInfo skip = TypeCashe.Queryable.Skip(_parameter.Type);

            int count = int.Parse(context.count.Text);

            return (IQueryable)skip.Invoke(null, new object[] { _sourceQueryable, count });
        }
    }
}
