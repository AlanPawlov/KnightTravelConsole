using System;
using System.Collections.Generic;



// Класс реализует алгоритм Варнсдорфа
// этот алгоритм означает что конь движется на то поле
// с которого можно пойти 
// на минимальное число еще не пройденных полей

class KnightTravel // knight(рыцарь) - конь в шахматах
{
    // массив шахматной доски
    // ячейки куда конь еще не наступал содержат 0,
    // когда на ячейку наступает конь, она помечается номером его хода
    ChessBoard chessBoard = new ChessBoard();

    // текущее место расположения коня
    // позиция коня будет рассматриваться как точка
    Point currentPoint = new Point();

    // номер хода
    int moveCount = 1;

    public KnightTravel()
    {
        // расчет ходов коня, начиная от клетки по координатам
        Calculate(0, 0);
    }

    // перегруженый конструктор
    public KnightTravel(int x, int y)
    {
        // расчет ходов коня, начиная от клетки по координатам
        Calculate(x, y);
    }

    // расчитать переходы коня
    void Calculate(int x, int y)
    {   // задаем первоначальную позицию
        SetPosition(x, y);

        // выполняем переходы пока все поле не закончится
        // как только поле закончится, .Next() вернет false
        while (Next());
    }


    // делает ход вперед, возвращает true или false
    bool Next()
    {
        // берем следующую позицию для коня
        Point nextPoint = GetNextPositionFromIt(chessBoard, currentPoint);

        if (nextPoint == null)
        {
            return false;
        }

        // устанавливаем коня на новую позицию
        return SetPosition(nextPoint);    
    }

    // метод устанавливает позицию
    // если позиция уже была занята, возвращает false
    bool SetPosition(Point newPoint)    
    {
        if (chessBoard.GetCell(newPoint) != 0)
        {
            // эта позиция уже занята
            return false;
        }

        // устанавливается новая позиция
        currentPoint = newPoint;

        // помечается место на котором конь уже был
        chessBoard.SetCell(currentPoint, moveCount);

        // номер текущего хода увеличивается на 1
        moveCount++;

        return true;
    }

    // перезагрузка метода
    bool SetPosition(int x, int y)
    {
        return SetPosition(new Point(x, y));
    }


    // функция определяет из какой точки можно
    // сделать меньше всего перемещений
    // и затем передает эту точку как следующую позицию
    Point GetNextPositionFromIt(ChessBoard inputBoard, Point position)
    {
        Point retPoint = null;
        
        // создается локальный board
        ChessBoard board = new ChessBoard(inputBoard); 

        // добывается массив возможных путей от текущей позиции
        List<Point> Ways = GetPossibleWaysFromIt(board, position);

        // инициализализируется с самым большим из возможных значений
        int lesserPointCount = 8;

        // ищем количество возможных путей от каждого из полученных путей
        // немного похоже на рекурсию
        // определяем путь из которого меньше всего новых путей
        // возвращаем его как лучшую точку для следующего хода
        foreach (Point point in Ways)
        {
            int pointCount = GetPossibleWaysFromIt(board, point).Count;
            if (lesserPointCount > pointCount)
            {
                lesserPointCount = pointCount;
                retPoint = point;
            }
        }

        return retPoint;
    }

    // возвращает массив из возможных путей
    List<Point> GetPossibleWaysFromIt(ChessBoard inputBoard, Point inputPoint)
    {
        List<Point> retWays = new List<Point>();

        // создание локальных копий объектов
        ChessBoard board = new ChessBoard(inputBoard);
        Point point = new Point(inputPoint);

        // маркировка поля от которого будут расчитываться ходы
        // нужно что бы программа не учитывала обратный ход как потенциальный
        board.SetCell(point, 1); 

        int x = point.x;
        int y = point.y;

        // расчет какие из потенциальных походов коня возможны
        // оциниваются поля со смещением +/- 1/2 (подобно ходу коня)

        // пригодные варианты заносятся в список

        // 1
        point.Set(x + 1, y + 2);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        //2
        point.Set(x + 1, y - 2);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 3
        point.Set(x + 2, y + 1);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 4
        point.Set(x + 2, y - 1);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 5
        point.Set(x - 1, y + 2);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 6
        point.Set(x - 1, y - 2);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 7
        point.Set(x - 2, y + 1);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        // 8
        point.Set(x - 2, y - 1);
        if (point.IsCorrect() && board.GetCell(point) == 0)
        {
            retWays.Add(new Point(point));
        }

        return retWays;
    }

    // возвращает массив шахматной доски
    public int[,] GetChessBoardArray()
    {
        return chessBoard.GetBoardArray();
    }

}

// реализует объект точки
// для обозначения шазматных полей
class Point
{
    // координаты
    public int x = 0;
    public int y = 0;

    // несколько перегруженных конструкторов
    public Point() { }

    public Point(int x_input, int y_input)
    {
        Set(x_input, y_input);
    }

    public Point(Point point)
    {
        Set(point.x, point.y);
    }

    // метод установки данных
    public void Set(int x_input, int y_input)
    {
        x = x_input;
        y = y_input;
    }

    // метод получения данных
    public int[] ToArray()
    {
        return new int[2] { x, y };
    }

    // проверяет что "x" и "y"
    // находятся в диапазоне от 0 до 7 (размер доски)
    public bool IsCorrect()
    {
        return (0 <= x) && (x <= 7)
                && (0 <= y) && (y <= 7);
    }

    // сравнивает точку с координатами
    public bool CompareWith(int in_x, int in_y)
    {
        return (x == in_x) && (y == in_y);
    }
}


// класс шахматной доски
class ChessBoard
{
    // массив доски
    int[,] board = new int[8, 8];

    // перегруженные конструкторы
    public ChessBoard() { }

    public ChessBoard(ChessBoard someBoard)
    {
        int[,] inputBoard = someBoard.GetBoardArray();
        for (int c = 0; c < 8; c++)
        {
            for (int r = 0; r < 8; r++)
            {
                board[c, r] = inputBoard[c ,r];
            }
        }
    }

    // перегружнные методы возвращения значения мклетки
    public int GetCell(Point point)
    {
        if (point.IsCorrect())
        {
            return board[point.x, point.y];
        } 
        
        return -1;
    }


    public int GetCell(int x, int y)
    {
        return GetCell(new Point(x, y));
    }

    // установка значения в клетку
    public void SetCell(Point point, int value)
    {
        if (point.IsCorrect())
        {
            board[point.x, point.y] = value;
        }
    }

    // получение массива доски
    public int[,] GetBoardArray()
    {
        return board;
    }

}



