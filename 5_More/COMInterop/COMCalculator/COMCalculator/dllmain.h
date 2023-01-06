// dllmain.h : Declaration of module class.

class CCOMCalculatorModule : public ATL::CAtlDllModuleT< CCOMCalculatorModule >
{
public :
	DECLARE_LIBID(LIBID_COMCalculatorLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_COMCALCULATOR, "{f08d76e8-32c4-4895-b49c-a3b11389f7c1}")
};

extern class CCOMCalculatorModule _AtlModule;
