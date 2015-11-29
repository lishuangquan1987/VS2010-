function Test2()
   local a=GetNewForm();
   --local a={2,2,2};
  -- msgbox(tostring(a[1]));
   --msgbox(tostring(a[2]));
  -- msgbox(tostring(a[3]));
  --a.Test();
  local b=GetColor("red");
  --a.msgbox("这是Lua脚本调用哦！")
  a.ForeColor=b;
  a.Text="这是Lua脚本调用哦！";
  --msgbox(type(a));
  return a;
end
