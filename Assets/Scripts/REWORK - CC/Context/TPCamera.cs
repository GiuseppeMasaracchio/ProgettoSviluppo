using UnityEngine;

public class TPCamera 
{
    private float xaxis;
    private float yaxis;

    private float camycurrent;
    private float camytarget;
    private float camylerp;

    private void UpdateCamera(GameObject cam, GameObject player, GameObject forward, Vector2 mouseInput, float sens) {
        VerticalSmoothCam(cam, player);

        CalculateMotion(mouseInput, sens);

        CamRotation(cam, forward);
    }

    private void CalculateMotion(Vector2 mouseInput, float sens) {
        yaxis += mouseInput.x * sens * Time.deltaTime;
        xaxis -= mouseInput.y * sens * Time.deltaTime;
        xaxis = Mathf.Clamp(xaxis, -20f, 80f);
    }

    private void CamRotation(GameObject cam, GameObject forward) {
        cam.transform.rotation = Quaternion.Euler(xaxis, yaxis, 0f);
        forward.transform.rotation = Quaternion.Euler(0f, yaxis, 0f);
    }

    private void VerticalSmoothCam(GameObject cam, GameObject player) {
        camycurrent = cam.transform.position.y;
        camytarget = player.transform.position.y;
        camylerp = Mathf.Lerp(camycurrent, camytarget, .025f);
        if (camycurrent < camytarget) {
            cam.transform.position = new Vector3(player.transform.position.x, camylerp, player.transform.position.z);
        }
        else {
            cam.transform.position = player.transform.position;
        }
    }
}
