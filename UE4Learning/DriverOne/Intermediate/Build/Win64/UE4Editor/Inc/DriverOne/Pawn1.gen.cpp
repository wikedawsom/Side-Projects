// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "UObject/GeneratedCppIncludes.h"
#include "DriverOne/Pawn1.h"
#ifdef _MSC_VER
#pragma warning (push)
#pragma warning (disable : 4883)
#endif
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodePawn1() {}
// Cross Module References
	DRIVERONE_API UClass* Z_Construct_UClass_APawn1_NoRegister();
	DRIVERONE_API UClass* Z_Construct_UClass_APawn1();
	ENGINE_API UClass* Z_Construct_UClass_APawn();
	UPackage* Z_Construct_UPackage__Script_DriverOne();
// End Cross Module References
	void APawn1::StaticRegisterNativesAPawn1()
	{
	}
	UClass* Z_Construct_UClass_APawn1_NoRegister()
	{
		return APawn1::StaticClass();
	}
	struct Z_Construct_UClass_APawn1_Statics
	{
		static UObject* (*const DependentSingletons[])();
#if WITH_METADATA
		static const UE4CodeGen_Private::FMetaDataPairParam Class_MetaDataParams[];
#endif
		static const FCppClassTypeInfoStatic StaticCppClassTypeInfo;
		static const UE4CodeGen_Private::FClassParams ClassParams;
	};
	UObject* (*const Z_Construct_UClass_APawn1_Statics::DependentSingletons[])() = {
		(UObject* (*)())Z_Construct_UClass_APawn,
		(UObject* (*)())Z_Construct_UPackage__Script_DriverOne,
	};
#if WITH_METADATA
	const UE4CodeGen_Private::FMetaDataPairParam Z_Construct_UClass_APawn1_Statics::Class_MetaDataParams[] = {
		{ "HideCategories", "Navigation" },
		{ "IncludePath", "Pawn1.h" },
		{ "ModuleRelativePath", "Pawn1.h" },
	};
#endif
	const FCppClassTypeInfoStatic Z_Construct_UClass_APawn1_Statics::StaticCppClassTypeInfo = {
		TCppClassTypeTraits<APawn1>::IsAbstract,
	};
	const UE4CodeGen_Private::FClassParams Z_Construct_UClass_APawn1_Statics::ClassParams = {
		&APawn1::StaticClass,
		nullptr,
		&StaticCppClassTypeInfo,
		DependentSingletons,
		nullptr,
		nullptr,
		nullptr,
		ARRAY_COUNT(DependentSingletons),
		0,
		0,
		0,
		0x009000A0u,
		METADATA_PARAMS(Z_Construct_UClass_APawn1_Statics::Class_MetaDataParams, ARRAY_COUNT(Z_Construct_UClass_APawn1_Statics::Class_MetaDataParams))
	};
	UClass* Z_Construct_UClass_APawn1()
	{
		static UClass* OuterClass = nullptr;
		if (!OuterClass)
		{
			UE4CodeGen_Private::ConstructUClass(OuterClass, Z_Construct_UClass_APawn1_Statics::ClassParams);
		}
		return OuterClass;
	}
	IMPLEMENT_CLASS(APawn1, 1753457717);
	template<> DRIVERONE_API UClass* StaticClass<APawn1>()
	{
		return APawn1::StaticClass();
	}
	static FCompiledInDefer Z_CompiledInDefer_UClass_APawn1(Z_Construct_UClass_APawn1, &APawn1::StaticClass, TEXT("/Script/DriverOne"), TEXT("APawn1"), false, nullptr, nullptr, nullptr);
	DEFINE_VTABLE_PTR_HELPER_CTOR(APawn1);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
#ifdef _MSC_VER
#pragma warning (pop)
#endif
