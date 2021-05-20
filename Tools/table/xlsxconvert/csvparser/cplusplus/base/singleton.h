

#ifndef BASE_SINGLETON_H_
#define BASE_SINGLETON_H_
#pragma once

namespace base
{
template <class T>
class Singleton
{
public:
  static T &get_instance()
  {
    static T instance;
    return instance;
  }
};

} // namespace base

#endif // BASE_SINGLETON_H_
