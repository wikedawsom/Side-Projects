// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/ObjectMacros.h"
#include "UObject/ScriptMacros.h"

PRAGMA_DISABLE_DEPRECATION_WARNINGS
#ifdef DRIVERONE_Pawn1_generated_h
#error "Pawn1.generated.h already included, missing '#pragma once' in Pawn1.h"
#endif
#define DRIVERONE_Pawn1_generated_h

#define DriverOne_Source_DriverOne_Pawn1_h_12_RPC_WRAPPERS
#define DriverOne_Source_DriverOne_Pawn1_h_12_RPC_WRAPPERS_NO_PURE_DECLS
#define DriverOne_Source_DriverOne_Pawn1_h_12_INCLASS_NO_PURE_DECLS \
private: \
	static void StaticRegisterNativesAPawn1(); \
	friend struct Z_Construct_UClass_APawn1_Statics; \
public: \
	DECLARE_CLASS(APawn1, APawn, COMPILED_IN_FLAGS(0), CASTCLASS_None, TEXT("/Script/DriverOne"), NO_API) \
	DECLARE_SERIALIZER(APawn1)


#define DriverOne_Source_DriverOne_Pawn1_h_12_INCLASS \
private: \
	static void StaticRegisterNativesAPawn1(); \
	friend struct Z_Construct_UClass_APawn1_Statics; \
public: \
	DECLARE_CLASS(APawn1, APawn, COMPILED_IN_FLAGS(0), CASTCLASS_None, TEXT("/Script/DriverOne"), NO_API) \
	DECLARE_SERIALIZER(APawn1)


#define DriverOne_Source_DriverOne_Pawn1_h_12_STANDARD_CONSTRUCTORS \
	/** Standard constructor, called after all reflected properties have been initialized */ \
	NO_API APawn1(const FObjectInitializer& ObjectInitializer); \
	DEFINE_DEFAULT_OBJECT_INITIALIZER_CONSTRUCTOR_CALL(APawn1) \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, APawn1); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(APawn1); \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API APawn1(APawn1&&); \
	NO_API APawn1(const APawn1&); \
public:


#define DriverOne_Source_DriverOne_Pawn1_h_12_ENHANCED_CONSTRUCTORS \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API APawn1(APawn1&&); \
	NO_API APawn1(const APawn1&); \
public: \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, APawn1); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(APawn1); \
	DEFINE_DEFAULT_CONSTRUCTOR_CALL(APawn1)


#define DriverOne_Source_DriverOne_Pawn1_h_12_PRIVATE_PROPERTY_OFFSET
#define DriverOne_Source_DriverOne_Pawn1_h_9_PROLOG
#define DriverOne_Source_DriverOne_Pawn1_h_12_GENERATED_BODY_LEGACY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	DriverOne_Source_DriverOne_Pawn1_h_12_PRIVATE_PROPERTY_OFFSET \
	DriverOne_Source_DriverOne_Pawn1_h_12_RPC_WRAPPERS \
	DriverOne_Source_DriverOne_Pawn1_h_12_INCLASS \
	DriverOne_Source_DriverOne_Pawn1_h_12_STANDARD_CONSTRUCTORS \
public: \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


#define DriverOne_Source_DriverOne_Pawn1_h_12_GENERATED_BODY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	DriverOne_Source_DriverOne_Pawn1_h_12_PRIVATE_PROPERTY_OFFSET \
	DriverOne_Source_DriverOne_Pawn1_h_12_RPC_WRAPPERS_NO_PURE_DECLS \
	DriverOne_Source_DriverOne_Pawn1_h_12_INCLASS_NO_PURE_DECLS \
	DriverOne_Source_DriverOne_Pawn1_h_12_ENHANCED_CONSTRUCTORS \
private: \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


template<> DRIVERONE_API UClass* StaticClass<class APawn1>();

#undef CURRENT_FILE_ID
#define CURRENT_FILE_ID DriverOne_Source_DriverOne_Pawn1_h


PRAGMA_ENABLE_DEPRECATION_WARNINGS
