using System.Collections;
using UnityEngine;

// ���� ����
public class Gun : MonoBehaviour {

    // ���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    // ����� ���� ������ �ʴ� �� "const" �� ��ǥ��
    // ���� Ÿ���� ���������̴�
    public enum State {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading, // ������ ��
    }

    // ������Ƽ
    public State state { get; private set; } // ���� ���� ����

    public Transform fireTransform; // ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffecct; // ź�� ���� ȿ��


    private LineRenderer bulletLineRenderrer; // ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer; // �� �Ҹ� �����

    public GunData gunData; // ���� ���� ������

    private float fireDistance = 50f; // �����Ÿ�

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� ���� �ִ� ź��

    private float lastFireTime; // ���� ���������� �߻��� ����

    private void Awake() {
        // ����� ������Ʈ�� ���� ��������
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderrer = GetComponent<LineRenderer>();

        // ����� ���� �� ���� ����
        bulletLineRenderrer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderrer.enabled = false;
    }

    private void OnEnable() {
        // �� ���� �ʱ�ȭ
        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        // ���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    // �߻� �õ�
    public void Fire() {


    }

    // ���� �߻� ó��
    private void Shot() {


    }

    // �߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition) {
        // �ѱ� ȭ�� ȿ�� ���
        muzzleFlashEffect.Play();
        // ź�� ���� ȿ�� ���
        shellEjectEffecct.Play();

        // �Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        // ���� �������� �ѱ��� ��ġ
        bulletLineRenderrer.SetPosition(0, fireTransform.position);
        // ���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderrer.SetPosition(1, hitPosition);


        
        // ���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderrer.enabled = true;

        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderrer.enabled = false;
    }

    // ������ �õ�
    public bool Reload() {
        return false;
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRoutine() {
        // ���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;

        // ������ �ҿ� �ð���ŭ ó�� ����
        yield return new WaitForSeconds(gunData.reloadTime);

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
