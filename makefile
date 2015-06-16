
# make me !

csc=mcs
bin=./nyanpass

classes=\
	nyanpass.cs\
	interpreter.cs\

all: $(bin)

$(bin): $(classes)
	$(csc) $(classes) /out:$(bin) /t:exe
