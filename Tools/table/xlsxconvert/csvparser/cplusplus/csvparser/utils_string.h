
#ifndef UTILS_STRING_H
#define UTILS_STRING_H

#include <string>
#include <cstdlib>
using namespace std;

static string Int2String(const int in_number) {
  string result;
  char str[32];
  memset(str, 0 , 32);
  sprintf(str, "%d", in_number);
  result = str;
  return result;
}

static bool IsHexadecimalString(const string &str)
{
  if(str.size() >= 2 && str[0] == '0' && str[1] == 'x')
    return true;
  return false;
}


static int String2Int(const string &str)
{
  return (int)atoi(str.c_str());
}

static bool String2Bool(const string &str)
{
  string result_str = str;
  if(result_str.compare("true") == 0 || result_str.compare("1") == 0)
    return true;
  else
     return false;
}

static float String2Float(const string &str)
{
  return (float)atof(str.c_str());
}


#endif