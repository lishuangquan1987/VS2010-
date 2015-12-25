#_*_coding:utf-8_*_
def yreadlines(filename):
    content="";
    f=file(filename,"r");
    for i in f.readlines():
        content=content+i+"\r\n"
    return content