// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/ObjectMacros.h"
#include "UObject/ScriptMacros.h"

PRAGMA_DISABLE_DEPRECATION_WARNINGS
#ifdef DRIVERONE_ActorOne_generated_h
#error "ActorOne.generated.h already included, missing '#pragma once' in ActorOne.h"
#endif
#define DRIVERONE_ActorOne_generated_h

#define DriverOne_Source_DriverOne_ActorOne_h_12_RPC_WRAPPERS
#define DriverOne_Source_DriverOne_ActorOne_h_12_RPC_WRAPPERS_NO_PURE_DECLS
#define DriverOne_Source_DriverOne_ActorOne_h_12_INCLASS_NO_PURE_DECLS \
private: \
	static void StaticRegisterNativesAActorOne(); \
	friend struct Z_Construct_UClass_AActorOne_Statics; \
public: \
	DECLARE_CLASS(AActorOne, AActor, COMPILED_IN_FLAGS(0), CASTCLASS_None, TEXT("/Script/DriverOne"), NO_API) \
	DECLARE_SERIALIZER(AActorOne)


#define DriverOne_Source_DriverOne_ActorOne_h_12_INCLASS \
private: \
	static void StaticRegisterNativesAActorOne(); \
	friend struct Z_Construct_UClass_AActorOne_Statics; \
public: \
	DECLARE_CLASS(AActorOne, AActor, COMPILED_IN_FLAGS(0), CASTCLASS_None, TEXT("/Script/DriverOne"), NO_API) \
	DECLARE_SERIALIZER(AActorOne)


#define DriverOne_Source_DriverOne_ActorOne_h_12_STANDARD_CONSTRUCTORS \
	/** Standard constructor, called after all reflected properties have been initialized */ \
	NO_API AActorOne(const FObjectInitializer& ObjectInitializer); \
	DEFINE_DEFAULT_OBJECT_INITIALIZER_CONSTRUCTOR_CALL(AActorOne) \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, AActorOne); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(AActorOne); \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API AActorOne(AActorOne&&); \
	NO_API AActorOne(const AActorOne&); \
public:


#define DriverOne_Source_DriverOne_ActorOne_h_12_ENHANCED_CONSTRUCTORS \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API AActorOne(AActorOne&&); \
	NO_API AActorOne(const AActorOne&); \
public: \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, AActorOne); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(AActorOne); \
	DEFINE_DEFAULT_CONSTRUCTOR_CALL(AActorOne)


#define DriverOne_Source_DriverOne_ActorOne_h_12_PRIVATE_PROPERTY_OFFSET
#define DriverOne_Source_DriverOne_ActorOne_h_9_PROLOG
#define DriverOne_Source_DriverOne_ActorOne_h_12_GENERATED_BODY_LEGACY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	DriverOne_Source_DriverOne_ActorOne_h_12_PRIVATE_PROPERTY_OFFSET \
	DriverOne_Source_DriverOne_ActorOne_h_12_RPC_WRAPPERS \
	DriverOne_Source_DriverOne_ActorOne_h_12_INCLASS \
	DriverOne_Source_DriverOne_ActorOne_h_12_STANDARD_CONSTRUCTORS \
public: \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


#define DriverOne_Source_DriverOne_ActorOne_h_12_GENERATED_BODY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	DriverOne_Source_DriverOne_ActorOne_h_12_PRIVATE_PROPERTY_OFFSET \
	DriverOne_Source_DriverOne_ActorOne_h_12_RPC_WRAPPERS_NO_PURE_DECLS \
	DriverOne_Source_DriverOne_ActorOne_h_12_INCLASS_NO_PURE_DECLS \
	DriverOne_Source_DriverOne_ActorOne_h_12_ENHANCED_CONSTRUCTORS \
private: \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


template<> DRIVERONE_API UClass* StaticClass<class AActorOne>();

#undef CURRENT_FILE_ID
#define CURRENT_FILE_ID DriverOne_Source_DriverOne_ActorOne_h


PRAGMA_ENABLE_DEPRECATION_WARNINGS
