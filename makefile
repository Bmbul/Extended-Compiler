MCS = mcs

OUT_EXEC = compiler.exe

COMMON_DIR = Compiler.Common
LEXICAL_ANALYSER_DIR = Compiler.LexicalAnalyser
PARSER_DIR = Compiler.Parser
GENERATOR_DIR = Compiler.Generator
COMPILER_DIR = Compiler

COMMON_SOURCES = $(shell find $(COMMON_DIR) -name '*.cs')
LEXICAL_SOURCES = $(shell find $(LEXICAL_ANALYSER_DIR) -name '*.cs')
PARSER_SOURCES = $(shell find $(PARSER_DIR) -name '*.cs')
GENERATOR_SOURCES = $(shell find $(GENERATOR_DIR) -name '*.cs')
COMPILER_SOURCES = $(shell find $(COMPILER_DIR) -name '*.cs')

COMMON_DLL = $(COMMON_DIR)/Compiler.Common.dll
GENERATOR_DLL = $(GENERATOR_DIR)/Compiler.Generator.dll
PARSER_DLL = $(PARSER_DIR)/Compiler.Parser.dll
LEXICAL_ANALYSER_DLL = $(LEXICAL_ANALYSER_DIR)/Compiler.LexicalAnalyser.dll

all: $(OUT_EXEC)

$(COMMON_DLL): $(COMMON_SOURCES)
	$(MCS) -target:library -out:$@ $^

$(LEXICAL_ANALYSER_DLL): $(LEXICAL_SOURCES) $(COMMON_DLL)
	$(MCS) -target:library -out:$@ $(LEXICAL_SOURCES) -r:$(COMMON_DLL)

$(GENERATOR_DLL): $(GENERATOR_SOURCES) $(COMMON_DLL)
	$(MCS) -target:library -out:$@ $(GENERATOR_SOURCES) -r:$(COMMON_DLL)

$(PARSER_DLL): $(PARSER_SOURCES) $(COMMON_DLL) $(GENERATOR_DLL)
	$(MCS) -target:library -out:$@ $(PARSER_SOURCES) -r:$(COMMON_DLL) -r:$(GENERATOR_DLL)

$(OUT_EXEC): $(COMPILER_SOURCES) $(PARSER_DLL) $(COMMON_DLL) $(GENERATOR_DLL) $(LEXICAL_ANALYSER_DLL)
	$(MCS) -out:$@ $(COMPILER_SOURCES) -r:$(COMMON_DLL) -r:$(GENERATOR_DLL) -r:$(PARSER_DLL) -r:$(LEXICAL_ANALYSER_DLL)

clean:
	rm -rf $(OUT_EXEC) $(COMMON_DLL) $(GENERATOR_DLL) $(PARSER_DLL) $(LEXICAL_ANALYSER_DLL)

.PHONY: all clean
