--FCT操作函数模块
--fct
local modname=...;
local M={};
_G[modname]=M;
package.loaded[modname]=M;

--Marco define
--instance is fixture
local DUT_LOG = 1;	--记录调试信息
local function DUT_log(fmt,...)
	if (DUT_LOG ~=0) then
		--Log("	:)"..string.format(fmt,...),ID);
        Log("	:)"..tostring(fmt),ID);
	end
end

function M.CheckError(str)
	
end

function M.SetDetectString(str)
	return FCTBroad:SetDetectString(str);
end
function M.WriteString(str)
	return FCTBroad:WriteString(str);
end

function M.ReadString()
	return FCTBroad:ReadString();
end

function M.WaitForString(ntime)
	return FCTBroad:WaitForString(ntime);
end

function M.WaitString(str,ntimeout)
	return FCTBroad:WaitString(str,ntimeout);
end

--Lex add you function below.