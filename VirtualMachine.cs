using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Befunge
{
    public class VirtualMachine
    {
        public void Run(PlayField playField, bool debug = false)
        {
            var stack = new Stack<long>();

            var stringMode = false;

            var randomForDirection = new Random();
            var availableDirections = new[] {
                ProgramCounterDirection.Up, ProgramCounterDirection.Down,
                ProgramCounterDirection.Left, ProgramCounterDirection.Right,
            };
            var currentDirection = ProgramCounterDirection.Right;

            while (true)
            {
                if (debug)
                {
                    if (!(playField.Current is BlankToken))
                    {
                        Console.Error.WriteLine(
                            $"Stack: {string.Join(", ", stack.ToList())} ");
                        Console.Error.WriteLine(playField.Current);
                    }
                }

                if (stringMode)
                {
                    if (playField.Current is QuoteToken)
                    {
                        stringMode = !stringMode;
                    }
                    else
                    {
                        stack.Push(playField.Current.AsCharToken().AsciiValue);
                    }
                }
                else
                {
                    long a, b;
                    switch (playField.Current)
                    {
                        case AddToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(a + b);
                            break;
                        case SubtractToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(b - a);
                            break;
                        case MultiplyToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(a * b);
                            break;
                        case DivideToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            if (a != 0)
                            {
                                stack.Push(b / a);
                            }
                            else
                            {
                                stack.Push(
                                    PromptForInt("Can't divide by zero. What result would you like?")
                                );
                            }
                            break;
                        case ModulusToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(b % a);
                            break;
                        case NotToken t:
                            a = stack.PopOrZero();
                            stack.Push(a == 0 ? 1 : 0);
                            break;
                        case GreaterToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(b > a ? 1 : 0);
                            break;
                        case RightToken t:
                            currentDirection = ProgramCounterDirection.Right;
                            break;
                        case LeftToken t:
                            currentDirection = ProgramCounterDirection.Left;
                            break;
                        case UpToken t:
                            currentDirection = ProgramCounterDirection.Up;
                            break;
                        case DownToken t:
                            currentDirection = ProgramCounterDirection.Down;
                            break;
                        case RandomToken t:
                            currentDirection =
                                availableDirections[randomForDirection.Next(availableDirections.Length)];
                            break;
                        case HorizontalIfToken t:
                            a = stack.PopOrZero();
                            currentDirection = a == 0
                                ? ProgramCounterDirection.Right
                                : ProgramCounterDirection.Left;
                            break;
                        case VerticalIfToken t:
                            a = stack.PopOrZero();
                            currentDirection = a == 0
                                ? ProgramCounterDirection.Down
                                : ProgramCounterDirection.Up;
                            break;
                        case QuoteToken t:
                            stringMode = !stringMode;
                            break;
                        case DuplicateToken t:
                            stack.Push(stack.PeekOrZero());
                            break;
                        case SwapToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(a);
                            stack.Push(b);
                            break;
                        case PopToken t:
                            stack.PopOrZero();
                            break;
                        case OutputIntToken t:
                            Console.Write(stack.PopOrZero());
                            break;
                        case OutputCharToken t:
                            Console.Write(char.ConvertFromUtf32((int)stack.PopOrZero()));
                            break;
                        case JumpToken t:
                            playField.ProgramCounter.Move(currentDirection);
                            break;
                        case GetToken t:
                            (a, b) = (stack.PopOrZero(), stack.PopOrZero());
                            stack.Push(
                                playField.IsLegalPosition(a, b)
                                    ? playField[a, b].AsCharToken().AsciiValue
                                    : 0
                            );
                            break;
                        case PutToken t:
                            long c;
                            (a, b, c) = (stack.PopOrZero(), stack.PopOrZero(), stack.PopOrZero());
                            char val = (char)c;
                            playField[a, b] = new CharToken(val, a, b);
                            break;
                        case InputIntToken t:
                            stack.Push(PromptForInt("Input a number"));
                            break;
                        case InputCharToken t:
                            stack.Push(PromptForChar("Input a character"));
                            break;
                        case NumberToken t:
                            stack.Push(t.Value);
                            break;
                        case BlankToken t:
                            break;
                        case HaltToken t:
                            return;
                        default:
                            throw new TokenizerException($"Unknown token: {playField.Current}");
                    }
                }
                playField.ProgramCounter.Move(currentDirection);
            }
        }

        private long PromptForInt(string prompt)
        {
            string response;
            long result;
            do
            {
                Console.Write($"{prompt} ");
                response = Console.ReadLine();
            } while (!long.TryParse(response, out result));
            return result;
        }

        private char PromptForChar(string prompt)
        {
            string response;
            do
            {
                Console.Write($"{prompt} ");
                response = Console.ReadLine();
            } while (response.Length > 0);
            return response[0];
        }
    }
}

