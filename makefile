
# make me !

csc=gmcs
output_exe ='./nyanpass'

classes=\
	nyanpass.cs\
	interpreter.cs\

release:
	@echo 'building...'
	@$(csc) $(classes) /out:$(output_exe) /t:exe
	@echo 'done ($(output_exe))'
