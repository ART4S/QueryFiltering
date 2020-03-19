using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Nodes.Base
{
    internal abstract class FunctionNode : BaseNode
    {
        private readonly MethodInfo _method;
        private readonly BaseNode[] _parameters;

        protected FunctionNode(MethodInfo method, BaseNode[] parameters)
        {
            _method = method;
            _parameters = parameters;
        }

        public override Expression CreateExpression()
        {
            return Expression.Call(
                _parameters[0].CreateExpression(),
                _method,
                _parameters.Skip(1).Select(x => x.CreateExpression()));
        }
    }
}
