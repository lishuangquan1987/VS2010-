Module="A145-FCT"			--Module Name
Version="Script Version 20151129 V1.0"						--Version add write factor
Customer="Apple"					--Customer ID
StationID=""					--Station ID
LineID=""					--Line ID
FixtureID=""					--Fixture ID
DirInLogPath="FCT"


local IDTable={};
OpenShortTable={};--二维table，根据Item和ID索引
DCRTable={};

function Test_OnEntry(par)		--Initial function for startup test,you can add test initial code in here

	--fct.WriteString("reset");
end

function Test_OnAbort(par)
	A34972.ResetOhmExecuted();
	A34972B.ResetOhmExecuted();
	A34972C.ResetOhmExecuted();
	A34972C.ResetOpenExecuted();
	fixture.FixtureUp();

end

function Test_OnFail(par)		--Clear function for test failed,you can add clear function code in here when test failed.
	A34972.ResetOhmExecuted();
	A34972B.ResetOhmExecuted();
	A34972C.ResetOhmExecuted();
	A34972C.ResetOpenExecuted();
	fixture.FixtureUp();
end

function Test_OnDone(par)		--Clear function for normal test finish.you can add clear function code in there when test normally finish.
	A34972.ResetOhmExecuted();
	A34972B.ResetOhmExecuted();
	A34972C.ResetOhmExecuted();
	A34972C.ResetOpenExecuted();
	fixture.FixtureUp();
	--fct.closePort();

end


function DCR_Real_ohm_Test(par)
	Lock();--lock all the uut,just wait here!
	if(A34972.checkOhmExecuted()==false) then
		A34972.setOhmExecuted();
		A34972.storyOhmData(par.cmd,3000);
	end
	if(A34972B.checkOhmExecuted()==false) then
		A34972B.setOhmExecuted();
		A34972B.storyOhmData(par.cmd,3000);
	end
	if(A34972C.checkOhmExecuted()==false) then
		A34972C.setOhmExecuted();
		A34972C.storyOhmData("MEAS:RES? 100,DEF,(@107:110,117:118)",3000);--"MEAS:RES? 100,DEF,(@107:110,117:118)"
	end
	UnLock();--unlock all the uut,all the thread can continue running.
end

function DCR_Test(par)
    local ret=nil;
    if(par.Item~=10) then
	   if(ID>=0 and ID<3) then
	    ret=A34972.GetOhmData(par.Item+ID*10)*1000;
	   else 
	    ret=A34972B.GetOhmData(par.Item+(ID-3)*10)*1000;
	   end
	elseif(par.Item==10) then
	    ret=A34972C.GetOhmData(ID)*1000;
	end	
	return ret;	
end



OpenShort_NetCMD={
  "SBIT062,061,060,059,066,065,064,063=0,0,1,0,0,1,1,1\r\n",--"VBUS"
  "SBIT062,061,060,059,066,065,064,063=0,0,1,1,0,1,1,1\r\n",--"VCON"
  "SBIT062,061,060,059,066,065,064,063=0,1,0,0,0,1,1,1\r\n",--"CC1"
  "SBIT062,061,060,059,066,065,064,063=0,1,0,1,0,1,1,1\r\n",--"DP1"--范围小。
  "SBIT062,061,060,059,066,065,064,063=0,1,1,0,0,1,1,1\r\n",--"DN1"
  "SBIT062,061,060,059,066,065,064,063=0,0,1,0,0,1,0,0\r\n",--"VBUSTOCC1"
  "SBIT062,061,060,059,066,065,064,063=0,0,1,0,0,0,1,1\r\n",--"VBUSTOVCON"
  "SBIT062,061,060,059,066,065,064,063=0,1,0,0,0,1,0,1\r\n",--"CC1TODP1"
  "SBIT062,061,060,059,066,065,064,063=0,1,0,1,0,1,1,0\r\n",--"DP1TODN1"
  "SBIT062,061,060,059,066,065,064,063=0,0,1,0,0,1,1,0\r\n"--"VBUSTODN1"
};

function OpenShort_Pretest()
    Lock();
	if(A34972C.checkOpenExecuted()==true)then
	   return;
	end
	OpenShortTable={};--清空table
	for key,value in ipairs(OpenShort_NetCMD) do
	   fct.WriteString(value);	   
	   if(key==4) then
	    A34972C.storyOpenData("MEAS:RES? 1000,DEF,(@107:110,117:118)",3000);
	   else 
	    A34972C.storyOpenData("MEAS:RES? 1M,DEF,(@107:110,117:118)",3000);-----量程？
	   end
	   local result=A34972C.GetOpenData()*1000;---------------待处理
	   table.insert(OpenShortTable,result);
	end
	A34972C.setOpenExecuted();
	UnLock();
end

function OpenShort_Test(par)
	return A34972C.GetOpenData[par.Item][ID]*1000;
end


function Standby_Cur_Test(par)
	 str = "MEAS:CURR:DC? 10mA,1E-6,";
	--string cmd = "";
	if(par==0) then
	cmd = str.."(@121)";
	elseif(par==1) then
	cmd = str.."(@122)";
	elseif(par==2) then
	cmd = str.."(@221)";
	elseif(par==3) then
	cmd = str.."(@222)";
	elseif(par==4) then
	cmd = str.."(@321)";
	elseif(par==5) then
	cmd = str.."(@322)";
	end
	Delay(300);
	return A34972.ReadCur(cmd)*1000000;
end
function Standby_Cur_PreTest(par)
	Lock();
	local ret,ret1,ret2,ret3,ret4,ret5= nil,nil,nil,nil,nil,nil;

	fct.WriteString("SBIT001=1\r\n");
	Delay(500);
	if (par.Vol==4.75) then
		fct.WriteString("SBIT002,003,004,005,006,007,008,009,022,023,024,025=0,1,0,1,0,1,0,1,0,1,0,1\r\n");
		Delay(500);
		if(par.Item=="Vcc1") then
			fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037=0,0,1,1,0,0,1,1,0,0,1,1\r\n");
			Delay(500);
			fct.WriteString("SBIT038,039,040,041,042,043,044,045,046,047,048,049=0,0,1,1,0,0,1,1,0,0,1,1\r\n");
		elseif(par.Item=="Vcon") then
			--fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037,038,039,040,041,042,043,044,045,046,047,048,049=0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0\r\n");
			fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037=0,1,1,0,0,1,1,0,0,1,1,0\r\n");
			Delay(500);
			fct.WriteString("SBIT038,039,040,041,042,043,044,045,046,047,048,049=0,1,1,0,0,1,1,0,0,1,1,0\r\n");
		end
	elseif(par.Vol==5.5) then
		fct.WriteString("SBIT002,003,004,005,006,007,008,009,022,023,024,025=1,0,1,0,1,0,1,0,1,0,1,0\r\n");
		Delay(500);
		if(par.Item=="Vcc1") then
			--fct.WriteString("SSBIT026,027,028,029,030,031,032,033,034,035,036,037,038,039,040,041,042,043,044,045,046,047,048,049=1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1\r\n");
			fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037=1,0,1,1,1,0,1,1,1,0,1,1\r\n");
			Delay(500);
			fct.WriteString("SBIT038,039,040,041,042,043,044,045,046,047,048,049=1,0,1,1,1,0,1,1,1,0,1,1\r\n");
		elseif(par.Item=="Vcon") then
			--fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037,038,039,040,041,042,043,044,045,046,047,048,049=1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0\r\n");
			fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037=1,1,1,0,1,1,1,0,1,1,1,0\r\n");
			Delay(500);
			fct.WriteString("SBIT038,039,040,041,042,043,044,045,046,047,048,049=1,1,1,0,1,1,1,0,1,1,1,0\r\n");
		end
	end
	Delay(1000);
	ret= Standby_Cur_Test(par.id);
	-- local resultTable={};
	-- for i=1,10,1 do
	  -- ret= Standby_Cur_Test(par.id);
	  -- table.insert(resultTable,ret);
	-- end
	-- ret=Calculate_Average(resultTable);

	fct.WriteString("SBIT001=0\r\n");
	Delay(500);
	fct.WriteString("SBIT002,003,004,005,006,007,008,009,022,023,024,025=1,1,1,1,1,1,1,1,1,1,1,1\r\n");
	Delay(500);
	fct.WriteString("SBIT026,027,028,029,030,031,032,033,034,035,036,037=0,0,0,0,0,0,0,0,0,0,0,0,0\r\n");
	Delay(500);
	fct.WriteString("SBIT038,039,040,041,042,043,044,045,046,047,048,049=0,0,0,0,0,0,0,0,0,0,0,0,0\r\n");
	Delay(500);
	UnLock();
	return ret;
end



function DisconverID_Test_Pre(par)
	Lock();
		if (par.id==0) then
			fct.WriteString("SBIT016,017,018,019,020,021=1,0,0,0,0,0\r\n");
		elseif (par.id==1)then
			fct.WriteString("SBIT016,017,018,019,020,021=0,1,0,0,0,0\r\n");
		elseif (par.id==2) then
			fct.WriteString("SBIT016,017,018,019,020,021=0,0,1,0,0,0\r\n");
		elseif (par.id==3)then
			fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,1,0,0\r\n");
		elseif (par.id==4) then
			 fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,0,1,0\r\n");
		elseif (par.id==5)then
			fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,0,0,1\r\n");
		end
		Delay(2000);
		IDTable[ID]=CYPress.getID();
	UnLock();
end

function Calculate_Average(Get_Table)
    if(Get_Table[1]==nil) then
	Get_Table={0};
	end
	local max=Get_Table[1];
	local min=Get_Table[1];
	local sum=0;
	local count=0;
	for a,b in ipairs(Get_Table) do
	  count=a;
	  sum=sum+b;
	  if(b>min) then
	    if(b>max) then
		max=b;
		end
	  else
	    if(b<min) then
		min=b;
		end
	  end
	end
	local average=(sum-max-min)/(count-2);
	return tonumber(average);
end

function DisconverID_Test(par)
	return IDTable[par.id][par.index];
end

function IDRead_VolSet(par)
	Lock();
	if (par.Vol==4.75) then
		fct.WriteString("SBIT002,003,004,005,006,007,008,009,022,023,024,025=0,1,0,1,0,1,0,1,0,1,0,1\r\n");
	elseif(par.Vol==5.5) then
		fct.WriteString("SBIT002,003,004,005,006,007,008,009,022,023,024,025=1,0,1,0,1,0,1,0,1,0,1,0\r\n");
	end
	Delay(100);
	if (par.id==0) then
			 fct.WriteString("SBIT026,027,028,029=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=1,0,0,0,0,0\r\n");
	elseif (par.id==1)then
			 fct.WriteString("SBIT030,031,032,033=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=0,1,0,0,0,0\r\n");
	elseif (par.id==2) then
			 fct.WriteString("SBIT034,035,036,037=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=0,0,1,0,0,0\r\n");
	elseif (par.id==3)then
			 fct.WriteString("SBIT038,039,040,041=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,1,0,0\r\n");
	elseif (par.id==4) then
			 fct.WriteString("SBIT042,043,044,045=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,0,1,0\r\n");
	elseif (par.id==5)then
			 fct.WriteString("SBIT046,047,048,049=0,1,1,1\r\n");
			 Delay(100);
			 fct.WriteString("SBIT016,017,018,019,020,021=0,0,0,0,0,1\r\n");
	end
	Delay(1000);
	IDTable[par.id]=CYPress.getID();
	UnLock();
end

local DCR_Item_Sub={
	{name="Real Ohm Test",lower=nil,upper=nil,unit="mOhm",entry=DCR_Real_ohm_Test,parameter={cmd="MEAS:FRES? 100,DEF,(@101:110,201:210,301:310)"},visible=0},
	{name="PP_USBC_VBUS_A4_Impedance",lower=30,upper=60,unit="mOhm",entry=DCR_Test,parameter={Item=0},visible=1},
	{name="PP_USBC_VBUS_A9_Impedance",lower=4,upper=16,unit="mOhm",entry=DCR_Test,parameter={Item=1},visible=1},
	{name="PP_USBC_VBUS_B4_Impedance",lower=45,upper=70,unit="mOhm",entry=DCR_Test,parameter={Item=2},visible=1},
	{name="PP_USBC_VBUS_B9_Impedance",lower=50,upper=80,unit="mOhm",entry=DCR_Test,parameter={Item=3},visible=1},
	{name="GND_A1_Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=4},visible=1},
	{name="GND_A12_Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=5},visible=1},
	{name="GND_B1_Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=6},visible=1},
	{name="GND_A12_Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=7},visible=1},
	{name="USBC_CC1(TP0309)Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=8},visible=1},
	{name="USBC_DP1(J0200_Pin_3)Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=9},visible=1},
	{name="USBC_DN1(J0200_Pin_4)Impedance",lower=25,upper=55,unit="mOhm",entry=DCR_Test,parameter={Item=10},visible=1},
};


local Standby_Item_Sub={
	--{name="Standby_Cur_PreTest",lower=nil,upper=nil,unit=nil,entry=Standby_Cur_PreTest,parameter={Vol=4.75,Item=Vcc1,id=ID},visible=0},
	{name="Current CC1_4.75V(uA)",lower=-5,upper=40,unit="uA",entry=Standby_Cur_PreTest,parameter={Vol=4.75,Item="Vcc1",id=ID},visible=1},
	--{name="Standby_Cur_PreTest",lower=nil,upper=nil,unit=nil,entry=Standby_Cur_PreTest,parameter={Vol=4.75,Item=Vcon,id=ID},visible=0},
	{name="Current Vconn_4.75V(uA)",lower=200,upper=500,unit="uA",entry=Standby_Cur_PreTest,parameter={Vol=4.75,Item="Vcon",id=ID},visible=1},
	--{name="Standby_Cur_PreTest",lower=nil,upper=nil,unit=nil,entry=Standby_Cur_PreTest,parameter={Vol=5.5,Item=Vcc1,id=ID},visible=0},
	{name="Current CC1_5.5V(uA)",lower=-5,upper=40,unit="uA",entry=Standby_Cur_PreTest,parameter={Vol=5.5,Item="Vcc1",id=ID},visible=1},
	--{name="Standby_Cur_PreTest",lower=nil,upper=nil,unit=nil,entry=Standby_Cur_PreTest,parameter={Vol=5.5,Item=Vcon,id=ID},visible=0},
	{name="Current Vconn_5.5V(uA)",lower=200,upper=600,unit="uA",entry=Standby_Cur_PreTest,parameter={Vol=5.5,Item="Vcon",id=ID},visible=1},
};


local OpenShort_Item_Sub={
    {name="OpenShort_Pretest",lower=nil,upper=nil,unit="Ohm",entry=OpenShort_Pretest,parameter=nil,visible=0},
	{name="USB_DP1 to GND",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=1,id=ID},visible=1},

	{name="USB_DN1 to GND",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=2,id=ID},visible=1},

	{name="USBC_VBUS to GND",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=3,id=ID},visible=1},

	{name="USBC_VCONN to GND",lower=500,upper=1500,unit="Ohm",entry=OpenShort_Test,parameter={Item=4,id=ID},visible=1},

	{name="USBC_CC1 to GND",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=5,id=ID},visible=1},



	{name="USBC_VBUS to USBC_CC1",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=6,id=ID},visible=1},

	{name="USBC_VBUS to USBC_VCONN",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=7,id=ID},visible=1},

	{name="USBC_CC1 to USB_DP1",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=8,id=ID},visible=1},

	{name="USB_DP1 to USB_DN1",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=9,id=ID},visible=1},

	{name="USBC_VBUS to USB_DN1",lower=10000,upper=1000000,unit="Ohm",entry=OpenShort_Test,parameter={Item=10,id=ID},visible=1},
};

local DisconverID_Item_Sub={
	{name="VoltageSet",lower=nil,upper=nil,unit=nil,entry=IDRead_VolSet,parameter={id=ID,Vol=4.75},visible=0},
	{name="Read Buf[0] 4.75V",lower="0xff008041",upper="0xff008041",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=0},visible=1},
	{name="Read Buf[1] 4.75V",lower="0x1c0004b4",upper="0x1c0004b4",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=1},visible=1},
	{name="Read Buf[2] 4.75V",lower="00000000",upper="00000000",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=2},visible=1},
	{name="Read Buf[3] 4.75V",lower="0xf6810000",upper="0xf6810000",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=3},visible=1},
	{name="Read Buf[4] 4.75V",lower="0x082fb2",upper="0x082fb2",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=4},visible=1},
	{name="VoltageSet",lower=nil,upper=nil,unit=nil,entry=DisconverID_Test_Pre,parameter={Vol=5.5,id=ID},visible=0},
	{name="Read Buf[0] 5.5V",lower="0xff008041",upper="0xff008041",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=0},visible=1},
	{name="Read Buf[1] 5.5V",lower="0x1c0004b4",upper="0x1c0004b4",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=1},visible=1},
	{name="Read Buf[2] 5.5V",lower="00000000",upper="00000000",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=2},visible=1},
	{name="Read Buf[3] 5.5V",lower="0xf6810000",upper="0xf6810000",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=3},visible=1},
	{name="Read Buf[4] 5.5V",lower="0x082fb2",upper="0x082fb2",unit=nil,entry=DisconverID_Test,parameter={id=ID,index=4},visible=1},
};

local DCR_Item={name="DCR_Item",entry=nil,parameter=nil,sub=DCR_Item_Sub};

local Standby_Item={name="Standby_Item",entry=nil,parameter=nil,sub=Standby_Item_Sub};

local OpenShort_Item={name="OpenShort_Item",entry=nil,parameter=nil,sub=OpenShort_Item_Sub};

local DisconverID_Item={name="DisconverID_Item",entry=nil,parameter=nil,sub=DisconverID_Item_Sub};

items =
{
   DisconverID_Item,
   OpenShort_Item,
   DCR_Item,---ok

   Standby_Item


}
