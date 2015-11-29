

function Main()

for i=0,100 do
Lock();
updateCheckBoxes(tostring(i),ID);
DbgOut("this is "..tostring(i),ID);
 if(i==99) then
 i=0;
 end
 Delay(1000);
UnLock();

end

end
