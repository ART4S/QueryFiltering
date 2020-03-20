using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QueryFiltering.Helpers
{
    internal static class TypeCashe
    {
        private static readonly ConcurrentDictionary<string, MethodInfo> Storage = new ConcurrentDictionary<string, MethodInfo>();

        private static string GetStorageKey(Type[] arguments, [CallerMemberName] string callerMethod = null)
            => $"{callerMethod}: {string.Join(", ", arguments.Select(x => x.Name))}";

        public static class Expression
        {
            public static MethodInfo LambdaFunc(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Expressions.Expression)
                        .GetMethods()
                        .First(x => x.Name == "Lambda")
                        .MakeGenericMethod(typeof(Func<,>)
                            .MakeGenericType(arguments));
                });
            }
        }

        public static class Queryable
        {
            public static MethodInfo Where(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "Where" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo Take(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "Take" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo Skip(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "Skip" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo Select(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "Select" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo OrderBy(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo OrderByDescending(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo ThenBy(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "ThenBy" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }

            public static MethodInfo ThenByDescending(params Type[] arguments)
            {
                return Storage.GetOrAdd(GetStorageKey(arguments), _ =>
                {
                    return typeof(System.Linq.Queryable)
                        .GetMethods()
                        .First(x => x.Name == "ThenByDescending" && x.GetParameters().Length == 2)
                        .MakeGenericMethod(arguments);
                });
            }
        }

        public static class String
        {
            public static MethodInfo EndsWith { get; } = typeof(string)
                .GetMethods()
                .First(x => x.Name == "EndsWith" && x.GetParameters()[0].ParameterType == typeof(string));

            public static MethodInfo StartsWith { get; } = typeof(string)
                .GetMethods()
                .First(x => x.Name == "StartsWith" && x.GetParameters()[0].ParameterType == typeof(string));

            public static MethodInfo ToLower { get; } = typeof(string)
                .GetMethods()
                .First(x => x.Name == "ToLower" && x.GetParameters().Length == 0);

            public static MethodInfo ToUpper = typeof(string)
                .GetMethods()
                .First(x => x.Name == "ToUpper" && x.GetParameters().Length == 0);
        }

        public static class Dictionary<TKey, TValue>
        {
            public static MethodInfo Add()
            {
                return Storage.GetOrAdd(GetStorageKey(new Type[] { }), _ =>
                 {
                     return typeof(System.Collections.Generic.Dictionary<TKey, TValue>)
                         .GetMethod("Add");
                 });
            }
        }
    }
}
