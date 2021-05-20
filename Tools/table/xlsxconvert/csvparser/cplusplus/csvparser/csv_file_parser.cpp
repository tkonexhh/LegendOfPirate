
#include "csv_file_parser.h"


CSVFileParser::CSVFileParser(void)
  :cdelimiter_(',')
  ,clinebreaker_('\n')
  ,cquoter_('\"')
  ,iskip_line_count_(0)
  ,pfile_(NULL)
  ,strfileName_("")
  ,bhas_more_line_(false)
  ,ireaded_line_count_(0)
{
}

CSVFileParser::~CSVFileParser(void)
{
  if(pfile_)
  {
    fclose(pfile_);
  }
}

bool CSVFileParser::InitWithFileName(const char *fullPath)
{  
	pfile_ = fopen(fullPath, "rb");
  if(pfile_ == NULL)
  {
	 //assert(false);
    //CCLog("Error: unable open file %s\n", fullPath.c_str());
    return false;
  }
  bhas_more_line_ = !(fgetc(pfile_) == EOF);
  rewind(pfile_);
  ireaded_line_count_ = 0;

  //skip line
  if(iskip_line_count_ > 0)
  {
    char *buffer = NULL;
    unsigned int buffer_length = 0;
    while (bhas_more_line_ && ireaded_line_count_ < iskip_line_count_)
    {
      read_single_line(&buffer, &buffer_length);
      ++ireaded_line_count_;
    }
  }

  return true;
}

void CSVFileParser::GetNextRow(CSVFileRow &row)
{
  char *buffer = NULL;
  unsigned int buffer_length = 0;

  row.clear();
  if(!bhas_more_line_ && pfile_)
    return;

  //skip rows that start with ';'
  do 
  {
    read_single_line(&buffer, &buffer_length);
    parse_single_line(buffer, buffer_length, row);
    ++ireaded_line_count_;
    free(buffer);
  } while (bhas_more_line_ && row.size() > 0 && row[0].size() > 0 && row[0][0] == ';');
}

void CSVFileParser::read_single_line(char **buffer, unsigned int *buffer_len)
{
  long int start_pos = ftell(pfile_);
  long int cur_pos = start_pos;

  register int cur_char = '\0';
  *buffer = NULL;
  *buffer_len = 0;
  int   quoter_num = 0;
  int   temp_quoter_num = 0;
  char  temp[8096];
  int   temp_count = 0;
  memset(temp, 0, sizeof(char) * 8096);

  while(true)
  {
    cur_char = fgetc(pfile_);
    temp[temp_count++] = cur_char;

    if(cur_char == EOF)
    {
      bhas_more_line_ = false;
      break;
    }
    else if(cur_char == clinebreaker_)
    {
      //read a field that cross mulit line
      if(quoter_num % 2 == 0)
      {  
        quoter_num = 0;
        break;
      }
      else
      {
        ++cur_pos;
      }
    }
    else
    {
      ++cur_pos;
      if(cur_char == cquoter_)
      {
        ++quoter_num;
        ++temp_quoter_num;
      }
    }
  }

  if(bhas_more_line_)
  {
    cur_char = fgetc(pfile_);
    bhas_more_line_ = !(cur_char == EOF);
  }

  if(cur_pos - start_pos > 0)
  {
    *buffer_len = cur_pos - start_pos;
    *buffer = (char*)malloc(sizeof(char) * (*buffer_len + 1));
    fseek(pfile_, start_pos, SEEK_SET);
    fread(*buffer, sizeof(char), *buffer_len, pfile_);

    //read \n
    fgetc(pfile_);

    
    //memset(temp, 0, sizeof(char) * 8096);
    memcpy(temp, *buffer, sizeof(char) * (*buffer_len));
    *(*buffer + *buffer_len) = '\0';

    //delete \r, end of line
    if(*buffer_len > 0 && *(*buffer + *buffer_len - 1) == '\r')
      --(*buffer_len);
  }
}

void CSVFileParser::parse_single_line(const char *buffer, const long int buffer_len, CSVFileRow &row)
{
  row.clear();
  if(buffer == NULL || buffer_len == 0)
  {
    return;
  }

  int   char_pos = 0;
  int   fild_length = 0;
  int   quote_fild_start = 0;
  int   non_quoter_fild_start = 0;
  bool  is_has_quoter = false;
  char  *fild_buffer = (char*)malloc(sizeof(char) * (buffer_len + 1));

  while(char_pos <= buffer_len)
  {
    //has quoter
    if(buffer[char_pos] == cquoter_)
    {
      //first quoter
      if(!is_has_quoter)
      {
        is_has_quoter = true;
        quote_fild_start = char_pos + 1;
      }
      //close quoter
      else
      {
        is_has_quoter = false;
        memcpy(fild_buffer, buffer + quote_fild_start, char_pos - quote_fild_start);
        fild_buffer[char_pos - quote_fild_start] = '\0';
        string str = fild_buffer;
        row.push_back(str);

        //reset none_quoter_fild_start
        non_quoter_fild_start = char_pos + 2;
        //skip quoter
        ++char_pos;
      }
    }
    else if(buffer[char_pos] == cdelimiter_ || char_pos == buffer_len)
    {
      if(!is_has_quoter)
      {
        memcpy(fild_buffer, buffer + non_quoter_fild_start, char_pos - non_quoter_fild_start);
        fild_buffer[char_pos - non_quoter_fild_start] = '\0';
        string str(fild_buffer);
        row.push_back(str);

        non_quoter_fild_start = char_pos + 1;
      }
    }
    ++char_pos;
  }

  free(fild_buffer);
}
