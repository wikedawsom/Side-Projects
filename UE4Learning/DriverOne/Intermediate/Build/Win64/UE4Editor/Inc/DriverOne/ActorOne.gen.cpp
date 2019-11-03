// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "DriverOne/ActorOne.h"
#ifdef _MSC_VER
#pragma warning (push)
#pragma warning (disable : 4883)
#endif
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeActorOne() {}
// Cross Module References
	DRIVERONE_API UClass* Z_Construct_UClass_AActorOne_NoRegister();
	DRIVERONE_API UClass* Z_Construct_UClass_AActorOne();
	ENGINE_API UClass* Z_Construct_UClass_AActor();
	UPackage* Z_Construct_UPackage__Script_DriverOne();
// End Cross Module References
	void AActorOne::StaticRegisterNativesAActorOne()
	{
	}
	UClass* Z_Construct_UClass_AActorOne_NoRegister()
	{
		return AActorOne::StaticClass();
	}
	struct Z_Construct_UClass_AActorOne_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam NewProp_DamagePerSecond_MetaData[];
#endif
		static const UE4CodeGen_Private::FFloatPropertyParams NewProp_DamagePerSecond;
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam NewProp_DamageTimeInSeconds_MetaData[];
#endif
		static const UE4CodeGen_Private::FFloatPropertyParams NewProp_DamageTimeInSeconds;
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam NewProp_TotalDamage_MetaData[];
#endif
		static const UE4CodeGen_Private::FIntPropertyParams NewProp_TotalDamage;
		static const UE4CodeGen_Private::FPropertyParamsBase* const PropPointers[];
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UE4CodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_AActorOne_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_AActor,
		(UObject* (*)())Z_Construct_UPackage__Script_DriverOne,
	};
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AActorOne_Statics::Class_MetaDataParams[] = {
		{ "IncludePath", "ActorOne.h" },
		{ "ModuleRelativePath", "ActorOne.h" },
	};
#endif
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AActorOne_Statics::NewProp_DamagePerSecond_MetaData[] = {
		{ "Category", "Damage" },
		{ "ModuleRelativePath", "ActorOne.h" },
	};
#endif
	const UE4CodeGen_Private::FFloatPropertyParams Z_Construct_UClass_AActorOne_Statics::NewProp_DamagePerSecond = { "DamagePerSecond", nullptr, (EPropertyFlags)0x0010000000022015, UE4CodeGen_Private::EPropertyGenFlags::Float, RF_Public|RF_Transient|RF_MarkAsNative, 1, STRUCT_OFFSET(AActorOne, DamagePerSecond), METADATA_PARAMS(Z_Construct_UClass_AActorOne_Statics::NewProp_DamagePerSecond_MetaData, ARRAY_COUNT(Z_Construct_UClass_AActorOne_Statics::NewProp_DamagePerSecond_MetaData)) };
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AActorOne_Statics::NewProp_DamageTimeInSeconds_MetaData[] = {
		{ "Category", "Damage" },
		{ "ModuleRelativePath", "ActorOne.h" },
	};
#endif
	const UE4CodeGen_Private::FFloatPropertyParams Z_Construct_UClass_AActorOne_Statics::NewProp_DamageTimeInSeconds = { "DamageTimeInSeconds", nullptr, (EPropertyFlags)0x0010000000000005, UE4CodeGen_Private::EPropertyGenFlags::Float, RF_Public|RF_Transient|RF_MarkAsNative, 1, STRUCT_OFFSET(AActorOne, DamageTimeInSeconds), METADATA_PARAMS(Z_Construct_UClass_AActorOne_Statics::NewProp_DamageTimeInSeconds_MetaData, ARRAY_COUNT(Z_Construct_UClass_AActorOne_Statics::NewProp_DamageTimeInSeconds_MetaData)) };
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_AActorOne_Statics::NewProp_TotalDamage_MetaData[] = {
		{ "Category", "Damage" },
		{ "ModuleRelativePath", "ActorOne.h" },
	};
#endif
	const UE4CodeGen_Private::FIntPropertyParams Z_Construct_UClass_AActorOne_Statics::NewProp_TotalDamage = { "TotalDamage", nullptr, (EPropertyFlags)0x0010000000000005, UE4CodeGen_Private::EPropertyGenFlags::Int, RF_Public|RF_Transient|RF_MarkAsNative, 1, STRUCT_OFFSET(AActorOne, TotalDamage), METADATA_PARAMS(Z_Construct_UClass_AActorOne_Statics::NewProp_TotalDamage_MetaData, ARRAY_COUNT(Z_Construct_UClass_AActorOne_Statics::NewProp_TotalDamage_MetaData)) };
	const UE4CodeGen_Private::FPropertyParamsBase* const Z_Construct_UClass_AActorOne_Statics::PropPointers[] = {
		(const UE4CodeGen_Private::FPropertyParamsBase*)&Z_Construct_UClass_AActorOne_Statics::NewProp_DamagePerSecond,
		(const UE4CodeGen_Private::FPropertyParamsBase*)&Z_Construct_UClass_AActorOne_Statics::NewProp_DamageTimeInSeconds,
		(const UE4CodeGen_Private::FPropertyParamsBase*)&Z_Construct_UClass_AActorOne_Statics::NewProp_TotalDamage,
	};
	const FCppClassTypeInfoStatic Z_Construct_UClass_AActorOne_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<AActorOne>::IsAbstract,
	};
	const UE4CodeGen_Private::FClassParams Z_Construct_UClass_AActorOne_Statics::ClassParams = {
		&AActorOne::StaticClass,
		nullptr,
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		Z_Construct_UClass_AActorOne_Statics::PropPointers,
		nullptr,
		ARRAY_COUNT(DependentSingletons),
		0,
		ARRAY_COUNT(Z_Construct_UClass_AActorOne_Statics::PropPointers),
		0,
		0x009000A0u,
		METADATA_PARAMS(Z_Construct_UClass_AActorOne_Statics::Class_MetaDataParams, ARRAY_COUNT(Z_Construct_UClass_AActorOne_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_AActorOne()
	{
		static UClass* OuterClass = nullptr;
		if (!OuterClass)
		{
			UE4CodeGen_Private::ConstructUClass(OuterClass, Z_Construct_UClass_AActorOne_Statics::ClassParams);
		}
		return OuterClass;
	}
	IMPLEMENT_CLASS(AActorOne, 4192689874);
	template<> DRIVERONE_API UClass* StaticClass<AActorOne>()
	{
		return AActorOne::StaticClass();
	}
	static FCompiledInDefer Z_CompiledInDefer_UClass_AActorOne(Z_Construct_UClass_AActorOne, &AActorOne::StaticClass, TEXT("/Script/DriverOne"), TEXT("AActorOne"), false, nullptr, nullptr, nullptr);
	DEFINE_VTABLE_PTR_HELPER_CTOR(AActorOne);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
#ifdef _MSC_VER
#pragma warning (pop)
#endif
