"""
    CookieDataStructure
    Copyright (C) 2026 Pe4enbka008 (Helen Ivanova)

    This code is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    See the GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
"""


class CookieError(Exception):
    # basically Interface - for try/catch
    def __init__(self, message):
        super().__init__(message)


class CookieEmptyStructureException(CookieError):
    def __init__(self, structure):
        super().__init__(f'{structure} has no values')


class CookieValueNotFoundException(CookieError):
    def __init__(self, key):
        super().__init__(f"Key '{key}' was not found")


class CookieIndexOutOfRangeException(CookieError):
    def __init__(self, index):
        super().__init__(f'Index {index} is out of range')


class CookieStructureArgumentException(CookieError):
    def __init__(self, reason):
        """
         :param reason:
        :type reason: str
        """
        super().__init__(reason)

