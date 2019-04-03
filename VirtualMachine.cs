using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Befunge
{
    public class VirtualMachine
    {

        public void Run(PlayField playField)
        {
            var stack = new Stack<int>();

            var isInString = false;

            var randomForDirection = new Random();
            var availableDirections = new[] {
                ProgramCounterDirection.Up, ProgramCounterDirection.Down,
                ProgramCounterDirection.Left, ProgramCounterDirection.Right,
            };
            var currentDirection = ProgramCounterDirection.Right;

            while (true)
            {
                //Console.WriteLine(playField.Current);
                //stack.ToList().ForEach(Console.WriteLine);

                if (isInString)
                {
                    if (playField.Current is QuoteToken) {
                        isInString = !isInString;
                    } else {
                        stack.Push(playField.Current.AsCharToken().AsciiValue);
                    }
                }
                else
                {
                    int a, b;
                    switch (playField.Current)
                    {
                        case AddToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(a + b);
                            break;
                        case SubtractToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(b - a);
                            break;
                        case MultiplyToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(a * b);
                            break;
                        case DivideToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
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
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(b % a);
                            break;
                        case NotToken t:
                            a = stack.Pop();
                            stack.Push(a == 0 ? 1 : 0);
                            break;
                        case GreaterToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
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
                            a = stack.Pop();
                            currentDirection = a == 0
                                ? ProgramCounterDirection.Right
                                : ProgramCounterDirection.Left;
                            break;
                        case VerticalIfToken t:
                            a = stack.Pop();
                            currentDirection = a == 0
                                ? ProgramCounterDirection.Down
                                : ProgramCounterDirection.Up;
                            break;
                        case QuoteToken t:
                            isInString = !isInString;
                            break;
                        case DuplicateToken t:
                            stack.Push(stack.Peek());
                            break;
                        case SwapToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(b);
                            stack.Push(a);
                            break;
                        case PopToken t:
                            stack.Pop();
                            break;
                        case OutputIntToken t:
                            Console.Write(stack.Pop());
                            break;
                        case OutputCharToken t:
                            Console.Write(char.ConvertFromUtf32(stack.Pop()));
                            break;
                        case JumpToken t:
                            playField.ProgramCounter.Move(currentDirection);
                            break;
                        case GetToken t:
                            (a, b) = (stack.Pop(), stack.Pop());
                            stack.Push(
                                playField.IsLegalPosition(a, b)
                                    ? playField[a, b].AsCharToken().AsciiValue
                                    : 0
                            );
                            break;
                        case PutToken t:
                            int c;
                            (a, b, c) = (stack.Pop(), stack.Pop(), stack.Pop());
                            char val = (char) c;
                            playField[a, b] = new CharToken(val, a, b);
                            break;
                        case InputIntToken t:
                            PromptForInt("Input a number ");
                            break;
                        case InputCharToken t:
                            PromptForInt("Input a character ");
                            break;
                        case NumberToken t:
                            stack.Push(t.Value);
                            break;
                        case BlankToken t:
                            break;
                        case HaltToken t:
                            return;
                        default:
                            throw new TokenizerException("Unknown token!");
                    }
                }
                playField.ProgramCounter.Move(currentDirection);
            }
        }

        private int PromptForInt(string prompt)
        {
            string response;
            int result;
            do
            {
                Console.Write($"{prompt} ");
                response = Console.ReadLine();
            } while (! int.TryParse(response, out result));
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

