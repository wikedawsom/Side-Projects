// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "ActorOne.generated.h"

UCLASS()
class DRIVERONE_API AActorOne : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AActorOne();
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category="Damage")
	int32 TotalDamage;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Damage")
	float DamageTimeInSeconds;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Transient, Category = "Damage")
	float DamagePerSecond;

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

	void PostInitProperties();

};
