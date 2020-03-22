using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Helpers;

namespace LinqToQuerystring.Visitors
{
    internal class TopVisitor : LinqToQuerystringBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public TopVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitTop(LinqToQuerystringParser.TopContext context)
        {
            MethodInfo take = TypeCashe.Queryable.Take(_parameter.Type);

            int count = int.Parse(context.count.Text);

            return (IQueryable)take.Invoke(null, new object[] { _sourceQueryable, count });
        }
    }
}
