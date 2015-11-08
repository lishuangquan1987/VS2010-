

function Main()

for i=0,100 do
Lock();
updateCheckBoxes(tostring(i));
DbgOut("this is "..tostring(i),GetID());
UnLock();
Delay(1000);
end

end
