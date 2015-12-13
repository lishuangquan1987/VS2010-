

function Main()
  local i=0;
  while(true) do

    updateCheckBoxes(tostring(i),ID);
    DbgOut("this is "..tostring(i),ID);
    if(i==99) then
      i=0;
    end
    Delay(1000);
    i=i+1;
  end

end
