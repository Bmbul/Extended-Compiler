.section .bss
.lcomm test_a, 8
.lcomm test_b, 8
.lcomm test_c, 8
.lcomm test_d, 8
.lcomm test_e, 8
.lcomm test_f, 8

.section .text
.globl _start
_start:

	MOVQ %rax, %rdi
	MOVQ $60, %rax
	SYSCALL
