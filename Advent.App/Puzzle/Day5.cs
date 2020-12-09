using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.App.Puzzle
{
    public class Day5
    {
        public static void PartOne(List<string> input)
        {
            var seats = new List<Seat>();
            foreach (var seat in input)
            {
                seats.Add(new Seat(seat));
            }

            var maxSeatCode = seats.Select(s => s.SeatId).Max();
            
            Console.WriteLine($"Highest seat id is: {maxSeatCode}");
        }

        public static void PartTwo(List<string> input)
        {
            var seats = new List<Seat>();
            foreach (var seat in input)
            {
                seats.Add(new Seat(seat));
            }

            var seatIds = seats.OrderBy(i => i.SeatId).Select(s => s.SeatId).ToList();
            var foundNumber = 0;
            for (int i = seatIds.Min(), j = 0; i < seatIds.Count(); i++, j++)
            {
                if (i != seatIds[j])
                {
                    foundNumber = i;
                    break;
                }
            }
            
            Console.WriteLine($"Found seat number: {foundNumber}");
        }
    }

    class Seat
    {
        private int _column;
        private int _row;

        private const int minRow = 0;
        private const int maxRow = 127;
        private const int minCol = 0;
        private const int maxCol = 7;

        public Seat(string ticketCode)
        {
            Calculate(ticketCode);
        }

        private void Calculate(string ticketCode)
        {
            var rowCode = ticketCode.ToCharArray().Take(7);
            var columnCode = ticketCode.ToCharArray().Skip(7);
            CalculateRowNumber(rowCode);
            CalculateColumnNumber(columnCode);
        }

        private void CalculateColumnNumber(IEnumerable<char> columnCode)
        {
            var currentMinColumn = minCol;
            var currentMaxColumn = maxCol;

            foreach (var code in columnCode)
            {
                if (code == 'R')
                {
                    currentMinColumn = ((currentMaxColumn - currentMinColumn) / 2) + currentMinColumn + 1;
                    if (currentMinColumn > currentMaxColumn)
                    {
                        var temp = currentMinColumn;
                        currentMinColumn = currentMaxColumn;
                        currentMaxColumn = temp;
                    }
                }
                else if (code == 'L')
                {
                    currentMaxColumn = ((currentMaxColumn - currentMinColumn) / 2) + currentMinColumn;
                    if (currentMinColumn > currentMaxColumn)
                    {
                        var temp = currentMinColumn;
                        currentMinColumn = currentMaxColumn;
                        currentMaxColumn = temp;
                    }
                }
                else
                    throw new InvalidOperationException($"Wrong column number code: {code}");
            }
            
            if (currentMaxColumn == currentMinColumn)
                _column = currentMaxColumn;
            else
                throw new InvalidOperationException();
        }

        private void CalculateRowNumber(IEnumerable<char> rowCode)
        {
            var currentRowMin = minRow; // 0 
            var currentRowMax = maxRow; // 127
            
            foreach (var code in rowCode)
            {
                if (code == 'B')
                {
                    currentRowMin = ((currentRowMax - currentRowMin) / 2) + currentRowMin + 1;
                    if (currentRowMin > currentRowMax)
                    {
                        var temp = currentRowMin;
                        currentRowMin = currentRowMax;
                        currentRowMax = temp;
                    }
                }
                else if (code == 'F')
                {
                    currentRowMax = ((currentRowMax - currentRowMin) / 2) + currentRowMin;
                    if (currentRowMin > currentRowMax)
                    {
                        var temp = currentRowMin;
                        currentRowMin = currentRowMax;
                        currentRowMax = temp;
                    }
                }
                else
                    throw new InvalidOperationException($"Wrong row number code: {code}");
            }

            if (currentRowMax == currentRowMin)
                _row = currentRowMax;
            else
                throw new InvalidOperationException();
        }

        public (int, int) SeatCoords => (_row, _column);
        
        public int SeatId => _row * 8 + _column;
    }
}