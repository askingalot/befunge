using System;
using System.Collections.Generic;

namespace Befunge
{
    public class VirtualMachine
    {
        private readonly PlayField _playField;
        private readonly Stack<int> _stack;

        public VirtualMachine(PlayField playField)
        {
            _playField = playField;
            _stack = new Stack<int>();
        }

        public void Run()
        {
            var randomForDirection = new Random();
            var availableDirections = new [] {
                ProgramCounterDirection.Up, ProgramCounterDirection.Down,
                ProgramCounterDirection.Left, ProgramCounterDirection.Right,
            };
            var currentDirection = ProgramCounterDirection.Right;

            while (true)
            {
                int a, b;
                switch (_playField.Current)
                {
                    case AddToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        _stack.Push(a + b);
                    break;
                    case SubtractToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        _stack.Push(b - a);
                    break;
                    case MultiplyToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        _stack.Push(a * b);
                    break;
                    case DivideToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        if (a != 0) {
                            _stack.Push(b / a);
                        } else {
                            _stack.Push(
                                PromptForInt("Can't divide by zero. What result would you like?")
                            );
                        }
                    break;
                    case ModulusToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        _stack.Push(b % a);
                    break;
                    case NotToken t:
                        a = _stack.Pop();
                        _stack.Push(a == 0 ? 1 : 0);
                    break;
                    case GreaterToken t:
                        (a, b) = (_stack.Pop(), _stack.Pop());
                        _stack.Push(b > a ? 1 : 0);
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
                    break;
                    case VerticalIfToken t:
                    break;
                    case QuoteToken t:
                    break;
                    case DuplicateToken t:
                    break;
                    case SwapToken t:
                    break;
                    case PopToken t:
                    break;
                    case OutputIntToken t:
                    break;
                    case OutputCharToken t:
                    break;
                    case JumpToken t:
                    break;
                    case GetToken t:
                    break;
                    case PutToken t:
                    break;
                    case InputIntToken t:
                    break;
                    case InputCharToken t:
                    break;
                    case NumberToken t:
                    break;
                    case CharToken t:
                    break;
                    case BlankToken t:
                    break;
                    case HaltToken t:
                        return;
                    default:
                        throw new TokenizerException("Unknown token!");
                }

                _playField.ProgramCounter.Move(currentDirection);
            }
        }

        private int PromptForInt(string prompt) {
            string response;
            int result;
            do {
                Console.Write($"{prompt} ");
                response = Console.ReadLine();
            } while(int.TryParse(response, out result));
            return result;
        }
    }
}

