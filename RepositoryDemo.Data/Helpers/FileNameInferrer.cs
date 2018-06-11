using System;

namespace RepositoryDemo.Data.Helpers
{
    internal static class FileNameInferrer
    {
        public static string InferFileName(this Type type, string extension) => $"{type.Name}.{extension}";
    }
}