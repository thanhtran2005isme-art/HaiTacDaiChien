using System;

namespace XLua
{
	internal static class TypeExtensions
	{
		public static bool IsValueType(this Type type)
		{
			return false;
		}

		public static bool IsEnum(this Type type)
		{
			return false;
		}

		public static bool IsPrimitive(this Type type)
		{
			return false;
		}

		public static bool IsAbstract(this Type type)
		{
			return false;
		}

		public static bool IsSealed(this Type type)
		{
			return false;
		}

		public static bool IsInterface(this Type type)
		{
			return false;
		}

		public static bool IsClass(this Type type)
		{
			return false;
		}

		public static Type BaseType(this Type type)
		{
			return null;
		}

		public static bool IsGenericType(this Type type)
		{
			return false;
		}

		public static bool IsGenericTypeDefinition(this Type type)
		{
			return false;
		}

		public static bool IsNestedPublic(this Type type)
		{
			return false;
		}

		public static bool IsPublic(this Type type)
		{
			return false;
		}

		public static string GetFriendlyName(this Type type)
		{
			return null;
		}
	}
}
