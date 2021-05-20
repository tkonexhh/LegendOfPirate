
#ifndef CSV_FILE_PARSER_H
#define CSV_FILE_PARSER_H

#include <string>
#include <vector>
#include <cstdio>
#include <cstring>
#include <cstdlib>

using namespace std;

typedef vector<string> CSVFileRow;

enum eCSV_QUOTE_TYPE {
  eCSV_QUOTE_TYPE_NONE,
  eCSV_QUOTE_TYPE_REQUIRED,
  eCSV_QUOTE_TYPE_OPTIONAL
};

class CSVFileParser
{
public:
  CSVFileParser(void);
  ~CSVFileParser(void);

public:
  //load csv file with file name(absolute path)
  bool    InitWithFileName(const char *my_fileName);
  //set delimiter for field
  void    SetDelimiter(char my_delimiter){cdelimiter_ = my_delimiter;}
  //set line breaker
  void    SetLineBreaker(char my_linebreaker){clinebreaker_ = my_linebreaker;}
  //set quoter for field
  void    SetQuoter(char my_quoter, eCSV_QUOTE_TYPE my_quote_type = eCSV_QUOTE_TYPE_OPTIONAL){cquoter_ = my_quoter;}
  //skip line count
  void    SetSkipLineCount(int my_skip_count){iskip_line_count_ = my_skip_count;}
  //has read file completed?
  bool    IsHasMoreLine(){return bhas_more_line_;}
  //get a parsed row
  void    GetNextRow(CSVFileRow &row);
  //get readed line count
  int     GetReadedLineCount(){return ireaded_line_count_;}

protected:
  //read single line data
  void    read_single_line(char **buffer, unsigned int *buffer_len);
  //parse line data to row
  void    parse_single_line(const char *buffer, const long int buffer_len, CSVFileRow &row);

private:
  char    cdelimiter_;
  char    clinebreaker_;
  char    cquoter_;
  int     iskip_line_count_;

  FILE    *pfile_;
  string  strfileName_;
  bool    bhas_more_line_;
  int     ireaded_line_count_;
};


#endif

