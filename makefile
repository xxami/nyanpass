
# make me !

csc=gmcs
output_exe ='./mewmew'

classes=\
	mewmew.cs\

release:
	@echo 'building...'
	@$(csc) $(classes) /out:$(output_exe) /t:exe
	@echo 'done ($(output_exe))'
