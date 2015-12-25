
function Dotest()
  method:SelectTextAndDelete(StartPoint.x,StartPoint.y,EndPoint.x,EndPoint.y);
  method:Delay(500);
  method:Paste(EndPoint.x,EndPoint.y,method._Text);
  method:Delay(500);
  method:ClickButton(ClickPositon.x,ClickPositon.y); 
end
-- lua["method"] = this;
--选中文字的前后坐标
StartPoint={x=498,y=254};
EndPoint={x=362,y=249};
--点击按钮位置
ClickPositon={x=657,y=191};
--要粘贴的内容
Content=
{
"[123]io set(1,bit31=1)",
"[123]io set(1,bit39=1)",
"[123]io set(1,bit23=1)",
"[123]io set(1,bit37=1)",
"[123]io set(1,bit32=1)",
"[123]io set(1,bit29=1)",
"[123]io set(1,bit42=1)",
"[123]io set(1,bit24=1)",
"[123]io set(1,bit26=1)",
"[123]io set(1,bit27=1)",
"[123]io set(1,bit28=1)",
"[123]io set(1,bit41=1)",
"[123]io set(1,bit44=1)",
"[123]io set(1,bit33=1)",
"[123]io set(1,bit38=1)",
"[123]io set(1,bit30=1)",
"[123]io set(1,bit18=1)",
"[123]io set(1,bit19=1)",
"[123]io set(1,bit35=1)",
"[123]io set(1,bit22=1)",
"[123]io set(1,bit17=1)",
"[123]io set(1,bit25=1)",
"[123]io set(1,bit46=1)"
}