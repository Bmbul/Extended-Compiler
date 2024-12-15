.section .rodata
fmt:
	.asciz "%d\n"

.section .bss
.lcomm test_a, 8
.lcomm test_b, 8
.lcomm test_c, 8
.lcomm test_d, 8
.lcomm test_e, 8
.lcomm test_f, 8
.lcomm test_g, 8

.section .text
.globl _start
_start:
	MOVQ $3, test_a
	MOVQ $2, test_b
	MOVQ $0, test_c
	MOVQ $5, test_d
	MOVQ $12, test_e
	MOVQ $11, test_f
	MOVQ $0, test_g
	MOVQ test_d, %rax
	ADDQ test_e, %rax
	MOVQ test_c, %rbx
	IMULQ %rax, %rbx
	MOVQ $5, %rax
	ADDQ test_f, %rax
	IMULQ %rax, %rbx
	MOVQ test_b, %rax
	SUBQ %rbx, %rax
	IMULQ test_g, %rax
	MOVQ test_a, %rbx
	ADDQ %rax, %rbx
	MOVQ %rbx, test_a

	MOVQ %rax, %rdi
	MOVQ $60, %rax
	SYSCALL
