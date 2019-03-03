/*
	THIS FILE IS A PART OF GTA V SCRIPT HOOK SDK
				http://dev-c.com			
			(C) Alexander Blade 2015
*/

#include "script.h"
#include "utils.h"

int idTextureSpeedoBack, idTextureSpeedoArrow;

int create_texture(std::string name)
{
	std::string path = GetCurrentModulePath(); // includes trailing slash
	return createTexture((path + name).c_str());
}

void create_textures()
{	
	idTextureSpeedoBack  = create_texture("NativeSpeedoBack.png");
	idTextureSpeedoArrow = create_texture("NativeSpeedoArrow.png");
}

void draw_speedo(float speed, float alpha)
{
	float rotation = speed * 2.51f /*as miles*/ * 1.6f /*as kilometers*/ / 320.0f /*circle max*/ + 0.655f /*arrow initial rotation*/;
	float screencorrection = GRAPHICS::_GET_SCREEN_ASPECT_RATIO(FALSE);
	drawTexture(idTextureSpeedoBack, 0, -9999, 100, 0.2f, 0.2f, 0.5f, 0.5f, 0.9f, 0.9f, 0.0f, screencorrection, 1.0f, 1.0f, 1.0f, alpha);
	drawTexture(idTextureSpeedoArrow, 0, -9998, 100, 0.25f, 0.25f, 0.5f, 0.5f, 0.9f, 0.9f, rotation, screencorrection, 1.0f, 1.0f, 1.0f, alpha);
}

float speedoAlpha;

void update()
{
	Player player = PLAYER::PLAYER_ID();
	Ped playerPed = PLAYER::PLAYER_PED_ID();

	// check if player ped exists and control is on (e.g. not in a cutscene)
	if (!ENTITY::DOES_ENTITY_EXIST(playerPed) || !PLAYER::IS_PLAYER_CONTROL_ON(player))
		return;

	// check for player ped death and player arrest
	if (ENTITY::IS_ENTITY_DEAD(playerPed) || PLAYER::IS_PLAYER_BEING_ARRESTED(player, TRUE))
		return;

	// check if player is in a vehicle and vehicle name isn't being displayed as well as player's phone
	const int HUD_VEHICLE_NAME = 6;
	if (!PED::IS_PED_IN_ANY_VEHICLE(playerPed, FALSE) || UI::IS_HUD_COMPONENT_ACTIVE(HUD_VEHICLE_NAME) || PED::IS_PED_RUNNING_MOBILE_PHONE_TASK(playerPed))
	{
		speedoAlpha = 0.0f;
		return;
	}

	// speedo alpha
	const float speedoAlphaMax = 0.8f;
	if (speedoAlpha < 0.0f) speedoAlpha = 0.0f;
	if (speedoAlpha < speedoAlphaMax) speedoAlpha += 0.01f;
	if (speedoAlpha > speedoAlphaMax) speedoAlpha = speedoAlphaMax;

	// speed
	float speed = ENTITY::GET_ENTITY_SPEED(PED::GET_VEHICLE_PED_IS_USING(playerPed));

	// draw textures
	draw_speedo(speed, speedoAlpha);
}

void main()
{	
	create_textures();
	while (true)
	{
		update();
		WAIT(0);
	}
}

void ScriptMain()
{
	main();
}
