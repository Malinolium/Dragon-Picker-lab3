# Лабораторная работа 2 [in GameDev]
Отчет по лабораторной работе #2 выполнил(а):
- Гулиева Эльвира Аразовна
- 1935822
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
Cоздание интерактивного приложения и изучение принципов интеграции в него игровых сервисов.

## Задание 1
### Интеграция сервиса для получения данных профиля пользователя.
Ход работы:
Задание 1
По теме видео практических работ 1-5 повторить реализацию игры на Unity.
Привести описание выполненных действий.


1. Создаю новый проект в unity hub.
2. Переименовала сцену
3. В менеджере пакетов подгрузила два ассета: "Dragon for boss monster" и "Fire & Spell";
4. Импортировала оба пакета
5. Разместила префаб синего дракона
6. Переместила в окно иерархии и переименовала
7. Разместила на необходимых координатах
8. Подключила animated controller
9. Подключила анимацию полета дракона в аниматор
10. Подключила анимацию к префабу дракона
11. Создала сферу - драконье яйцо
12. Накинула материал для драконьего яйца
13. Добавила rigid body в префаб яйца
14. Создала новый тег для драконьего яйца
15. Создала новую сферу для энергетического щита и расставила необходимые координаты
16. Накинула новый материал для щита
17. Изменила рендеринг мод
18. Подключила rigid body, отключила gravity, включила kinematic
19. Настроила камеру как в видео
20. ПОставила соотношение сторон в отображении игры 16:9
21. Создала скрипт для дракона, чтобы он перемещался в разные стороны и сбрасывал яйца


```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_dragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed = 1;
    public float timeBetweenEggDrops = 1f;
    public float leftRightDistanse = 10f;
    public float chanceDirection = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropEgg", 2f);
    }

    void DropEgg(){
        Vector3 myVector = new Vector3(0.0f, 5.0f, 0.0f);
        GameObject egg = Instantiate<GameObject>(dragonEggPrefab);
        egg.transform.position = transform.position + myVector;
        Invoke("DropEgg", timeBetweenEggDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistanse){
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistanse){
            speed = -Mathf.Abs(speed);
        }
    }
    private void FixedUpdate(){
        if (Random.value < chanceDirection){
            speed *= -1;
        }
    }
}

```
22. Подключила скрипт к дракону
23. Поместила префаб яйца в скрипт перемещения дракона
24. Расставила необходимые значения для сбрасывания яйца и скорость пермещения дракона
25. Описала работу яйца


```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonEgg : MonoBehaviour
{
    public static float bottomY = -30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY){
            Destroy(this.gameObject);
        }
    }
}

```

28. Добавила эффекты взрыва с помощью ассета Fire & Spell
29. Накинула текстуру взрыва, настроила рендеринг
30. Настроила particle system
31. ![image](https://user-images.githubusercontent.com/57430501/194327359-528bbb62-b699-421f-8688-79e13210a2f6.png)
32. Добавила работу энергетического щита - сгенерированные 3 слоя


```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;

    public int numEnergyShield = 3;

    public float energyShieldBottomY = -6f;

    public float energyShieldRadius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= numEnergyShield; i++){
            GameObject tShieldGo = Instantiate<GameObject>(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

```


![2022-10-04-17-35-17_Trim-_2_](https://user-images.githubusercontent.com/57430501/194357940-7fcb1e9b-6bd6-4dd3-baea-432b701af584.gif)




## Задание 2
В проект, выполненный в предыдущем задании, добавить систему проверки того, что SDK подключен (доступен в режиме онлайн и отвечает на запросы);

После того, как я сбилдила проект, создался файл index.html
Для подключения SDK я использовала Instant Game Bridge. Ссылка на гит: https://github.com/instant-games-bridge/instant-games-bridge
Далее я поместила кусок кода в Index.html и добавила сообщения-логи.



```
<script src="https://cdn.jsdelivr.net/gh/instant-games-bridge/instant-games-bridge@1.5.2/dist/instant-games-bridge.js"></script>
<script>
    bridge.initialize()
        .then(() => {
            // Initialized. You can use other methods.
        })
        .catch(error => {
            // Error
        })
</script>
```

Открываем страницу. Открываем консоль. Бинго


![image](https://user-images.githubusercontent.com/57430501/194382200-ce9a7498-75d0-44fa-9554-18d88540ff3b.png)


```
```




Вторая часть работы (или ее альтернативный вариант) состоял в том, что релиз-плагин с гитхаба скачиваешь и открываешь в юнити. Там как раз и находится скрипт Bridge, из которого должны подтягиваться функции. При написании скрипта пользовалась документацией, но после билда в браузере сыпется куча ошибок.

```
using InstantGamesBridge;
using UnityEngine;

public class log_bridge : MonoBehaviour
{
private void Start()
{
    Debug.Log("Hello world");
    Bridge.Initialize(success =>
    {
        if (success)
        {
           Debug.Log("Initializate");
        }
        else
        {
            Debug.Log("Error");
        }
    });
}
}
```

![image](https://user-images.githubusercontent.com/57430501/194392514-3f09045f-d062-4d8b-823a-08a959e055be.png)





## Задание 3
1. Произвести сравнительный анализ игровых сервисов Яндекс Игры
и VK Game;

Яндекс.Игры — это каталог браузерных игр, которые можно запускать как на мобильных телефонах, так и на компьютерах. Яндекс встроит каталог игр в главную страницу, а также в Яндекс.Браузер и приложение Яндекс.
Игры оптимизированы для декстопной и мобильной веб-версии.
Присутствует официальный SDK.

Игровая платформа VK — это 10 миллионов активных пользователей, которые ежемесячно проводят время в играх ВКонтакте. Встраиваемую игру можно запустить мгновенно — ни ввода пароля, ни скачивания на телефон, ни ожидания установки.
Игры доступны пользователям:
в десктопной (vk.com) и мобильной версии (m.vk.com),
в приложении ВКонтакте для iOS (iPadOS) и Android.
Присутвует официальный VK Bridge и VK SDK для разных языков: JS, Java, PHP, IOS и Android
![image](https://user-images.githubusercontent.com/57430501/194359352-248ec2ba-8685-41a1-985c-97b4566c458c.png)



- Яндекс.Игры на мой взгляд масштабнее, есть поддержка разных языков, тщательная модерация игр. Присутствует свой SDK. Для подключения необходим плагин внешний. Вк шустрее, но работает более локально. Поживее коммуникация - но это обычные люди в группках, комьюнити поживее. Искать нормальный рабочий SDK пришлось на просторах DTF.

- Весь отчет выше является рефератом. Т.к. по-человечески нет возможности пощупать ни vk game, ни Яндекс.Игры; 



```
```

## Выводы
- Абзац умных слов о том, что было сделано и что было узнано.
Я амбассадор хейта юнити.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Powered by

**BigDigital Team: Denisov | Fadeev | Panov**
