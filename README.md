# О проекте
Данный проект - две библиотеки классов, написанные на языке программирование C#. Первая позволяет работать с комплексными числами и кватернионами, а также вращать некоторую точку в пространстве, что реализовано в классе кватернионов. Вторая библиотека классов - инструмент для чтения моделей из OBJ-файлов, вращения данных моделей и перезаписывания OBJ-файлов с с возможностью добавлять в них модели.

## Содержание
- [Технологии](#технологии)
- [Начало работы](#начало-работы)
- [Тестирование](#тестирование)
- [Contributing](#contributing)
- [To do](#to-do)
- [Команда проекта](#команда-проекта)

## Технологии
- [C#](https://learn.microsoft.com/ru-ru/dotnet/csharp/)
- [.obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file)

## Использование
Расскажите как установить и использовать ваш проект, покажите пример кода:

Установите npm-пакет с помощью команды:
```sh
$ npm i your-awesome-plugin-name
```

И добавьте в свой проект:
```typescript
import { hi } from "your-awesome-plugin-name";

hi(); // Выведет в консоль "Привет!"
```

## Разработка

### Требования
Для установки и запуска проекта, необходим [NodeJS](https://nodejs.org/) v8+.

### Установка зависимостей
Для установки зависимостей, выполните команду:
```sh
$ npm i
```

## Тестирование
Какие инструменты тестирования использованы в проекте и как их запускать. Например:

Наш проект покрыт юнит-тестами Jest. Для их запуска выполните команду:
```sh
npm run test
```

### Зачем вы разработали этот проект?
Чтобы освоиться в той области знаний, которая раньше мне была неведома. Мне было интересно.

## To do
- [x] Добавить хоть какое-то README
- [ ] Дописать README
- [ ] Продолжить работать над проектом (добавить возможности по прочей работе с моделями из OBJ-файлов)
- [ ] Разобраться с разделом FAQ
- [ ] Добавить тестирование

## Команда проекта
- [Согоян Вазген](https://t.me/iheaytdg) — на момент создания проекта - ученик 11 класса.

## Источники
Я вдохновлялся индивидуальным проектом прошлого года, который был про решение уравнений 2-й, 3-й и 4-й степеней с комплексными коэффициентами и так же был реализован на C#․ Понять (хотя бы отчасти) кватернионы во многом мне помог плейлист в ютубе, выложенный на канале ЦИТМ Экспонента. Это "Цикл лекций о великих математиках", который ведет Алексей Савватеев. 