using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Functions
{
    internal class EndsWithNode : FunctionNode
    {
        public EndsWithNode(BaseNode[] parameters) : base(TypeCashe.String.EndsWith, parameters)
        {
        }
    }
}
