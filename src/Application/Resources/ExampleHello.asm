jmp main

hello:
  db "Hello, world!", 0

main:
  mov si, hello
  mov di, 0x8000
  call print

  hlt

print:
  mov al, [si]
  jmp .loop_cond

  .loop:
  mov al, [si]
  mov [di], al
  inc si
  inc di

  .loop_cond:
  cmp al, 0
  jne .loop
  
  ret
