function run()
  while(true) do
  t=os.date("*t");
  local str=t.hour..":"..t.min..":"..t.sec;
  updateTextBox(str);
  Delay(1000);
  end
end