
A= require "test1"

function Eat()
 return people:Eat();
end

function cry()
 people.name="jjj";
 return people:cry();
end

function showA(a,b)
  local result=A.add(a,b);
  people:msgbox(tostring(result));
end
