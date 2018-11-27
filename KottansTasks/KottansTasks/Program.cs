using System;

namespace KottansTasks
{
    class CreditCard
    {
        private string card_id;
        public CreditCard(string val_of_card_id)
        {
            card_id = val_of_card_id;
        }


        public string GenerateNext(string card_number)
        {
            string name_of_vendor = GetVendor(card_number);
            int size = card_number.Length;
            int[] arr = new int[size-1];
            Random r = new Random();
            string next_number_card=null;

            switch (name_of_vendor)
            {
                case "JCB":
                    arr[0] = 3;
                    arr[1] = 5;
                    for(int i = 2; i<size-1; i++)
                    {
                        if (i == 2)
                        {
                            arr[i] = r.Next(2, 8);
                        }

                        if(i==3 && arr[i-1]==2)
                        {
                            arr[i] = r.Next(8, 9);
                        }
                        else
                        {
                            arr[i] = r.Next(0, 9);
                        }

                        
                        if(i>3)
                        {
                            arr[i] = r.Next(0, 9);
                        }
                    }
                    break;

                case "Visa":
                    arr[0] = 4;
                    for(int i = 1; i<size-1; i++ )
                    {
                        arr[i] = r.Next(0, 9);
                    }
                    break;

                case "American express":
                    arr[0] = 3;

                    if(card_id[1] == '4')
                    {
                        arr[1] = 7;
                    }
                    else
                    {
                        arr[1] = 4;
                    }

                    for(int i = 2; i<size-1; i++)
                    {
                        arr[i] = r.Next(0, 9);
                    }
                    break;

                case "Maestro":
                    arr[0] = r.Next(5, 6);

                    if(arr[0] == 5)
                    {
                        if(card_id[1] == '0')
                        {
                            arr[1] = r.Next(6, 8);
                        }
                        else
                        {
                            arr[1] = 0;
                        }

                        for(int i = 2; i < size-1; i++)
                        {
                            arr[i] = r.Next(0, 9);
                        }
                    }
                    else
                    {
                       if(card_id[1] == '7')
                        {
                            arr[1] = 3;
                            arr[2] = 9;
                            for (int i = 3; i < size-1; i++)
                            {
                                arr[i] = r.Next(0, 9);
                            }
                        }
                        else
                        {
                            arr[1] = 7;

                            for (int i = 2; i < size-1; i++)
                            {
                                arr[i] = r.Next(0, 9);
                            }
                        }

                    }
                    break;

                case "Master Card":

                    if(card_id[0] == '2')
                    {
                        arr[0] = 5;
                        arr[1] = r.Next(1, 5);

                        for (int i = 2; i < size-1; i++)
                        {
                            arr[i] = r.Next(0, 9);
                        }
                    }
                    else
                    {
                        arr[0] = 2;
                        arr[1] = r.Next(2, 7);
                        arr[2] = 2;

                        for (int i = 3; i < size-1; i++)
                        {
                            arr[i] = r.Next(0, 9);
                        }
                    }
                    break;

                case "The length of you card is invalid":
                    return name_of_vendor;
                    break;

                case "Unknown":
                    return name_of_vendor;
                    break;
            }

            for(int i = 0; i < size-1; i++)
            {
                next_number_card += arr[i].ToString();
            }
            string valid_card = GenerateLastDigit(next_number_card);
            
            return valid_card;
        }

        private string GenerateLastDigit(string card_number)
        {

            
            string tmp = card_number.Replace(" ", "");
            int size = tmp.Length;

            if(size < 12 || size > 19)
            {
                return tmp;
            }


            int[] arr = new int[size];
            int sum_of_digits = 0;
            arr = ConvertToInt(tmp);

            for(int i = size-1; i >= 0; i = i-2)
            {
                arr[i] *= 2;
                if(arr[i]>9)
                {
                    arr[i] = arr[i] - 9;
                }
            }

            for(int i = 0; i < size; i++)
            {
               sum_of_digits += arr[i];
            }
            sum_of_digits *= 9;
            int last_digit = sum_of_digits % 10;
            
            tmp+= last_digit.ToString();

            return tmp; 
        }


        public bool IsValid(string card_number)
        {


            string tmp = card_number.Replace(" ", "");
            int size = tmp.Length;

            if (size < 12 || size > 19)
            {
                return false;
            }


            int[] arr = new int[size];
            int sum_of_digits = 0;
            arr = ConvertToInt(tmp);

            for (int i = size - 2; i >= 0; i = i - 2)
            {
                arr[i] *= 2;
                if (arr[i] > 9)
                {
                    arr[i] = arr[i] - 9;
                }
            }

            for (int i = 0; i < size; i++)
            {
                sum_of_digits += arr[i];
            }
            sum_of_digits *= 9;

            if (sum_of_digits % 10 == 0)
            {
                return true;
            }
            return false;
        }
        public int[] ConvertToInt(string str)
        {
            int size = str.Length;
            int[]arr = new int[size];
            for(int i=0; i < size; i++)
            {
                arr[i] = (int)str[i] - 48;
            }
            return arr;
        }
        public string GetVendor(string card_number)
        {
            string tmp = "Unknown";

            int size = card_number.Length;
            
            char tmp1 = card_number[0];
            switch (tmp1)
            {
                case '2':
                    if (size != 16)
                    {
                        tmp = "The length of you card is invalid";
                        return tmp;
                    }

                    for (int i = 2; i < 8; i++)
                    {
                        if (card_number[1].ToString() == i.ToString() && card_number[2] == '2')
                        {
                            tmp = "Master Card";
                        }
                    }
                    break;

                case '3':
                    if (size < 16 && size > 19)
                    {
                        tmp = "The length of you card is invalid";
                        return tmp;
                    }

                    if (card_number[1] == '5')
                    {

                        string tmp2 = card_number[2].ToString() + card_number[3].ToString();
                        for (int i = 28; i <= 89; i++)
                            if (tmp2 == i.ToString())
                            {
                                tmp = "JCB";
                                break;
                            }
                    }
                    else
                    {
                        if (card_number[1] == '4' || card_number[1] == '7')
                        {
                            tmp = "American express";
                        }
                    }
                    break;

                case '4':
                    if (size != 16)
                    {
                        tmp = "The length of you card is invalid";
                        return tmp;
                    }

                    tmp = "Visa";
                    break;

                case '5':
                    if (size < 12 && size > 19)
                    {
                        tmp = "The length of you card is invalid";
                        return tmp;
                    }

                    if (card_number[1] == '0' || card_number[1] == '6' || card_number[1] == '7' || card_number[1] == '8')
                      {
                        tmp = "Maestro";
                      }
                    
                    else
                    {
                        if (size != 16)
                        {
                            tmp = "The length of you card is invalid";
                            return tmp;
                        }

                        for (int i = 1; i <= 5; i++)
                            {
                                if (card_number[1].ToString() == i.ToString())
                                {
                                    tmp = "Master Card";
                                }
                            }
                    }
                    break;

                case '6':
                    if (size < 12 && size > 19)
                    {
                        tmp = "The length of you card is invalid";
                        return tmp;
                    }

                    if (card_number[1] == '7')
                    {
                        tmp = "Maestro";
                    }

                    else
                    {
                     if(card_number[1]=='3' && card_number[2]=='9')
                        {
                            tmp = "Maestro";
                        }
                    }
                    break;
                
                

            }
            return tmp;
        }


  }

      class Program
    {
        static void Main()
        {
            string number = "4026883624516838";
            CreditCard card = new CreditCard(number);
        
            string next_number = card.GenerateNext(number);
            string name_of_vendor = card.GetVendor(number);
            bool is_valid = card.IsValid(number);
            Console.WriteLine(name_of_vendor);
            Console.WriteLine(is_valid);
            Console.WriteLine(next_number);
         }
    }
}
