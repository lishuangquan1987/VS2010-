local module=...;
local M={};
_G[module]=M;
package.loaded[module]=M;

function add(a,b)
 return a+b;
end
