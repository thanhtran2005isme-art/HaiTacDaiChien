using System;
using System.Runtime.InteropServices;

namespace XLua.LuaDLL
{
	public class Lua
	{
		private const string LUADLL = "xlua";

		[PreserveSig]
		public static extern IntPtr lua_tothread(IntPtr L, int index);

		[PreserveSig]
		public static extern int xlua_get_lib_version();

		[PreserveSig]
		public static extern int lua_gc(IntPtr L, LuaGCOptions what, int data);

		[PreserveSig]
		public static extern IntPtr lua_getupvalue(IntPtr L, int funcindex, int n);

		[PreserveSig]
		public static extern IntPtr lua_setupvalue(IntPtr L, int funcindex, int n);

		[PreserveSig]
		public static extern int lua_pushthread(IntPtr L);

		public static bool lua_isfunction(IntPtr L, int stackPos)
		{
			return false;
		}

		public static bool lua_islightuserdata(IntPtr L, int stackPos)
		{
			return false;
		}

		public static bool lua_istable(IntPtr L, int stackPos)
		{
			return false;
		}

		public static bool lua_isthread(IntPtr L, int stackPos)
		{
			return false;
		}

		public static int luaL_error(IntPtr L, string message)
		{
			return 0;
		}

		[PreserveSig]
		public static extern int lua_setfenv(IntPtr L, int stackPos);

		[PreserveSig]
		public static extern IntPtr luaL_newstate();

		[PreserveSig]
		public static extern void lua_close(IntPtr L);

		[PreserveSig]
		public static extern void luaopen_xlua(IntPtr L);

		[PreserveSig]
		public static extern void luaL_openlibs(IntPtr L);

		[PreserveSig]
		public static extern uint xlua_objlen(IntPtr L, int stackPos);

		[PreserveSig]
		public static extern void lua_createtable(IntPtr L, int narr, int nrec);

		public static void lua_newtable(IntPtr L)
		{
		}

		[PreserveSig]
		public static extern int xlua_getglobal(IntPtr L, string name);

		[PreserveSig]
		public static extern int xlua_setglobal(IntPtr L, string name);

		[PreserveSig]
		public static extern void xlua_getloaders(IntPtr L);

		[PreserveSig]
		public static extern void lua_settop(IntPtr L, int newTop);

		public static void lua_pop(IntPtr L, int amount)
		{
		}

		[PreserveSig]
		public static extern void lua_insert(IntPtr L, int newTop);

		[PreserveSig]
		public static extern void lua_remove(IntPtr L, int index);

		[PreserveSig]
		public static extern int lua_rawget(IntPtr L, int index);

		[PreserveSig]
		public static extern void lua_rawset(IntPtr L, int index);

		[PreserveSig]
		public static extern int lua_setmetatable(IntPtr L, int objIndex);

		[PreserveSig]
		public static extern int lua_rawequal(IntPtr L, int index1, int index2);

		[PreserveSig]
		public static extern void lua_pushvalue(IntPtr L, int index);

		[PreserveSig]
		public static extern void lua_pushcclosure(IntPtr L, IntPtr fn, int n);

		[PreserveSig]
		public static extern void lua_replace(IntPtr L, int index);

		[PreserveSig]
		public static extern int lua_gettop(IntPtr L);

		[PreserveSig]
		public static extern LuaTypes lua_type(IntPtr L, int index);

		public static bool lua_isnil(IntPtr L, int index)
		{
			return false;
		}

		[PreserveSig]
		public static extern bool lua_isnumber(IntPtr L, int index);

		public static bool lua_isboolean(IntPtr L, int index)
		{
			return false;
		}

		[PreserveSig]
		public static extern int luaL_ref(IntPtr L, int registryIndex);

		public static int luaL_ref(IntPtr L)
		{
			return 0;
		}

		[PreserveSig]
		public static extern void xlua_rawgeti(IntPtr L, int tableIndex, long index);

		[PreserveSig]
		public static extern void xlua_rawseti(IntPtr L, int tableIndex, long index);

		public static void lua_getref(IntPtr L, int reference)
		{
		}

		[PreserveSig]
		public static extern int pcall_prepare(IntPtr L, int error_func_ref, int func_ref);

		[PreserveSig]
		public static extern void luaL_unref(IntPtr L, int registryIndex, int reference);

		public static void lua_unref(IntPtr L, int reference)
		{
		}

		[PreserveSig]
		public static extern bool lua_isstring(IntPtr L, int index);

		[PreserveSig]
		public static extern bool lua_isinteger(IntPtr L, int index);

		[PreserveSig]
		public static extern void lua_pushnil(IntPtr L);

		public static void lua_pushstdcallcfunction(IntPtr L, lua_CSFunction function, int n = 0)
		{
		}

		[PreserveSig]
		public static extern int xlua_upvalueindex(int n);

		[PreserveSig]
		public static extern int lua_pcall(IntPtr L, int nArgs, int nResults, int errfunc);

		[PreserveSig]
		public static extern double lua_tonumber(IntPtr L, int index);

		[PreserveSig]
		public static extern int xlua_tointeger(IntPtr L, int index);

		[PreserveSig]
		public static extern uint xlua_touint(IntPtr L, int index);

		[PreserveSig]
		public static extern bool lua_toboolean(IntPtr L, int index);

		[PreserveSig]
		public static extern IntPtr lua_topointer(IntPtr L, int index);

		[PreserveSig]
		public static extern IntPtr lua_tolstring(IntPtr L, int index, out IntPtr strLen);

		public static string lua_tostring(IntPtr L, int index)
		{
			return null;
		}

		[PreserveSig]
		public static extern IntPtr lua_atpanic(IntPtr L, lua_CSFunction panicf);

		[PreserveSig]
		public static extern void lua_pushnumber(IntPtr L, double number);

		[PreserveSig]
		public static extern void lua_pushboolean(IntPtr L, bool value);

		[PreserveSig]
		public static extern void xlua_pushinteger(IntPtr L, int value);

		[PreserveSig]
		public static extern void xlua_pushuint(IntPtr L, uint value);

		public static void lua_pushstring(IntPtr L, string str)
		{
		}

		[PreserveSig]
		public static extern void xlua_pushlstring(IntPtr L, byte[] str, int size);

		public static void xlua_pushasciistring(IntPtr L, string str)
		{
		}

		public static void lua_pushstring(IntPtr L, byte[] str)
		{
		}

		public static byte[] lua_tobytes(IntPtr L, int index)
		{
			return null;
		}

		[PreserveSig]
		public static extern int luaL_newmetatable(IntPtr L, string meta);

		[PreserveSig]
		public static extern int xlua_pgettable(IntPtr L, int idx);

		[PreserveSig]
		public static extern int xlua_psettable(IntPtr L, int idx);

		public static void luaL_getmetatable(IntPtr L, string meta)
		{
		}

		[PreserveSig]
		public static extern int xluaL_loadbuffer(IntPtr L, byte[] buff, int size, string name);

		public static int luaL_loadbuffer(IntPtr L, string buff, string name)
		{
			return 0;
		}

		[PreserveSig]
		public static extern int xlua_tocsobj_safe(IntPtr L, int obj);

		[PreserveSig]
		public static extern int xlua_tocsobj_fast(IntPtr L, int obj);

		public static int lua_error(IntPtr L)
		{
			return 0;
		}

		[PreserveSig]
		public static extern bool lua_checkstack(IntPtr L, int extra);

		[PreserveSig]
		public static extern int lua_next(IntPtr L, int index);

		[PreserveSig]
		public static extern void lua_pushlightuserdata(IntPtr L, IntPtr udata);

		[PreserveSig]
		public static extern IntPtr xlua_tag();

		[PreserveSig]
		public static extern void luaL_where(IntPtr L, int level);

		[PreserveSig]
		public static extern int xlua_tryget_cachedud(IntPtr L, int key, int cache_ref);

		[PreserveSig]
		public static extern void xlua_pushcsobj(IntPtr L, int key, int meta_ref, bool need_cache, int cache_ref);

		[PreserveSig]
		public static extern int gen_obj_indexer(IntPtr L);

		[PreserveSig]
		public static extern int gen_obj_newindexer(IntPtr L);

		[PreserveSig]
		public static extern int gen_cls_indexer(IntPtr L);

		[PreserveSig]
		public static extern int gen_cls_newindexer(IntPtr L);

		[PreserveSig]
		public static extern int get_error_func_ref(IntPtr L);

		[PreserveSig]
		public static extern int load_error_func(IntPtr L, int Ref);

		[PreserveSig]
		public static extern int luaopen_i64lib(IntPtr L);

		[PreserveSig]
		public static extern int luaopen_socket_core(IntPtr L);

		[PreserveSig]
		public static extern void lua_pushint64(IntPtr L, long n);

		[PreserveSig]
		public static extern void lua_pushuint64(IntPtr L, ulong n);

		[PreserveSig]
		public static extern bool lua_isint64(IntPtr L, int idx);

		[PreserveSig]
		public static extern bool lua_isuint64(IntPtr L, int idx);

		[PreserveSig]
		public static extern long lua_toint64(IntPtr L, int idx);

		[PreserveSig]
		public static extern ulong lua_touint64(IntPtr L, int idx);

		[PreserveSig]
		public static extern void xlua_push_csharp_function(IntPtr L, IntPtr fn, int n);

		[PreserveSig]
		public static extern int xlua_csharp_str_error(IntPtr L, string message);

		[PreserveSig]
		public static extern int xlua_csharp_error(IntPtr L);

		[PreserveSig]
		public static extern bool xlua_pack_int8_t(IntPtr buff, int offset, byte field);

		[PreserveSig]
		public static extern bool xlua_unpack_int8_t(IntPtr buff, int offset, out byte field);

		[PreserveSig]
		public static extern bool xlua_pack_int16_t(IntPtr buff, int offset, short field);

		[PreserveSig]
		public static extern bool xlua_unpack_int16_t(IntPtr buff, int offset, out short field);

		[PreserveSig]
		public static extern bool xlua_pack_int32_t(IntPtr buff, int offset, int field);

		[PreserveSig]
		public static extern bool xlua_unpack_int32_t(IntPtr buff, int offset, out int field);

		[PreserveSig]
		public static extern bool xlua_pack_int64_t(IntPtr buff, int offset, long field);

		[PreserveSig]
		public static extern bool xlua_unpack_int64_t(IntPtr buff, int offset, out long field);

		[PreserveSig]
		public static extern bool xlua_pack_float(IntPtr buff, int offset, float field);

		[PreserveSig]
		public static extern bool xlua_unpack_float(IntPtr buff, int offset, out float field);

		[PreserveSig]
		public static extern bool xlua_pack_double(IntPtr buff, int offset, double field);

		[PreserveSig]
		public static extern bool xlua_unpack_double(IntPtr buff, int offset, out double field);

		[PreserveSig]
		public static extern IntPtr xlua_pushstruct(IntPtr L, uint size, int meta_ref);

		[PreserveSig]
		public static extern void xlua_pushcstable(IntPtr L, uint field_count, int meta_ref);

		[PreserveSig]
		public static extern IntPtr lua_touserdata(IntPtr L, int idx);

		[PreserveSig]
		public static extern int xlua_gettypeid(IntPtr L, int idx);

		[PreserveSig]
		public static extern int xlua_get_registry_index();

		[PreserveSig]
		public static extern int xlua_pgettable_bypath(IntPtr L, int idx, string path);

		[PreserveSig]
		public static extern int xlua_psettable_bypath(IntPtr L, int idx, string path);

		[PreserveSig]
		public static extern bool xlua_pack_float2(IntPtr buff, int offset, float f1, float f2);

		[PreserveSig]
		public static extern bool xlua_unpack_float2(IntPtr buff, int offset, out float f1, out float f2);

		[PreserveSig]
		public static extern bool xlua_pack_float3(IntPtr buff, int offset, float f1, float f2, float f3);

		[PreserveSig]
		public static extern bool xlua_unpack_float3(IntPtr buff, int offset, out float f1, out float f2, out float f3);

		[PreserveSig]
		public static extern bool xlua_pack_float4(IntPtr buff, int offset, float f1, float f2, float f3, float f4);

		[PreserveSig]
		public static extern bool xlua_unpack_float4(IntPtr buff, int offset, out float f1, out float f2, out float f3, out float f4);

		[PreserveSig]
		public static extern bool xlua_pack_float5(IntPtr buff, int offset, float f1, float f2, float f3, float f4, float f5);

		[PreserveSig]
		public static extern bool xlua_unpack_float5(IntPtr buff, int offset, out float f1, out float f2, out float f3, out float f4, out float f5);

		[PreserveSig]
		public static extern bool xlua_pack_float6(IntPtr buff, int offset, float f1, float f2, float f3, float f4, float f5, float f6);

		[PreserveSig]
		public static extern bool xlua_unpack_float6(IntPtr buff, int offset, out float f1, out float f2, out float f3, out float f4, out float f5, out float f6);

		[PreserveSig]
		public static extern bool xlua_pack_decimal(IntPtr buff, int offset, ref decimal dec);

		[PreserveSig]
		public static extern bool xlua_unpack_decimal(IntPtr buff, int offset, out byte scale, out byte sign, out int hi32, out ulong lo64);

		public static bool xlua_is_eq_str(IntPtr L, int index, string str)
		{
			return false;
		}

		[PreserveSig]
		public static extern bool xlua_is_eq_str(IntPtr L, int index, string str, int str_len);

		[PreserveSig]
		public static extern IntPtr xlua_gl(IntPtr L);
	}
}
